1~
using AgendaNet.Auth.Domain.Models;
2~
using AgendaNet.Auth.Services;
3~
using Bogus;
4~
using Microsoft.Extensions.Configuration;
5~
using Microsoft.Extensions.Logging.Abstractions;
6~
using System.IdentityModel.Tokens.Jwt;
7~

8~
namespace AgendaNet_Tests.Integration
9~
{
10~
    public class TokenServiceIntegrationTests
11~
    {
12~
        private readonly TokenService _tokenService;
13~
        private readonly Faker _faker;
14~

15~
        public TokenServiceIntegrationTests()
16~
        {
17~
            var inMemorySettings = new Dictionary<string, string>
18~
            {
19~
                { "AUTHENTICATION", "b3a2448f94c74f57b8e7a3d4e59b89b932ab4f7db23e4928a76aa49f5311229f" }
20~
            };
21~

22~
            IConfiguration configuration = new ConfigurationBuilder()
23~
                .AddInMemoryCollection(inMemorySettings)
24~
                .Build();
25~

26~
            _tokenService = new TokenService(configuration, new NullLogger<TokenService>());
27~
            _faker = new Faker("pt_BR");
28~
        }
29~

30~
        [Fact(DisplayName = "Deve executar fluxo completo de geração, validação e extração de email")]
31~
        public void Deve_Executar_Fluxo_Completo()
32~
        {
33~
            // Arrange
34~
            var user = new UserViewModel
35~
            {
36~
                Email = _faker.Internet.Email(),
37~
                Stabilish_Id = _faker.Random.Guid().ToString(),
38~
                UserName = _faker.Internet.UserName(),
39~
                Password = _faker.Internet.Password()
40~
            };
41~

42~
            // Act - Gerar token
43~
            var tokens = _tokenService.GerarJwtToken(user, 60);
44~

45~
            // Act - Validar token
46~
            var principal = _tokenService.ValidarToken(tokens.Access_Token);
47~

48~
            // Act - Extrair email
49~
            var emailExtraido = _tokenService.ObterEmailToken(tokens.Access_Token);
50~

51~
            // Assert
52~
            Assert.NotNull(tokens);
53~
            Assert.NotNull(tokens.Access_Token);
54~
            Assert.NotNull(principal);
55~
            Assert.Equal(user.Email, emailExtraido);
56~
        }
57~

58~
        [Fact(DisplayName = "Deve gerar múltiplos tokens independentes para o mesmo usuário")]
59~
        public void Deve_Gerar_Multiplos_Tokens_Independentes()
60~
        {
61~
            // Arrange
62~
            var user = new UserViewModel
63~
            {
64~
                Email = _faker.Internet.Email(),
65~
                Stabilish_Id = _faker.Random.Guid().ToString()
66~
            };
67~

68~
            // Act
69~
            var tokens1 = _tokenService.GerarJwtToken(user, 30);
70~
            var tokens2 = _tokenService.GerarJwtToken(user, 30);
71~
            var tokens3 = _tokenService.GerarJwtToken(user, 30);
72~

73~
            // Assert
74~
            Assert.NotEqual(tokens1.Access_Token, tokens2.Access_Token);
75~
            Assert.NotEqual(tokens2.Access_Token, tokens3.Access_Token);
76~
            Assert.NotEqual(tokens1.JwtId, tokens2.JwtId);
77~

78~
            // Todos devem ser válidos independentemente
79~
            var principal1 = _tokenService.ValidarToken(tokens1.Access_Token);
80~
            var principal2 = _tokenService.ValidarToken(tokens2.Access_Token);
81~
            var principal3 = _tokenService.ValidarToken(tokens3.Access_Token);
82~

83~
            Assert.NotNull(principal1);
84~
            Assert.NotNull(principal2);
85~
            Assert.NotNull(principal3);
86~
        }
87~

88~
        [Fact(DisplayName = "Deve manter integridade de dados do usuário através do token")]
89~
        public void Deve_Manter_Integridade_Dados_Usuario()
90~
        {
91~
            // Arrange
92~
            var user = new UserViewModel
93~
            {
94~
                Email = _faker.Internet.Email(),
95~
                Stabilish_Id = _faker.Random.Guid().ToString(),
96~
                UserName = _faker.Internet.UserName()
97~
            };
98~

99~
            // Act
100~
            var tokens = _tokenService.GerarJwtToken(user, 60);
101~
            var principal = _tokenService.ValidarToken(tokens.Access_Token);
102~

103~
            // Assert - Verificar todas as claims
104~
            var emailClaim = principal.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
105~
            var subClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
106~
            var jtiClaim = principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
107~

108~
            Assert.Equal(user.Email, emailClaim);
109~
            Assert.Equal(user.Stabilish_Id, subClaim);
110~
            Assert.NotNull(jtiClaim);
111~
        }
112~

113~
        [Fact(DisplayName = "Deve processar sequencialmente múltiplas operações")]
114~
        public void Deve_Processar_Sequencialmente_Multiplas_Operacoes()
115~
        {
116~
            // Arrange
117~
            var users = Enumerable.Range(0, 10)
118~
                .Select(i => new UserViewModel
119~
                {
120~
                    Email = _faker.Internet.Email(),
121~
                    Stabilish_Id = _faker.Random.Guid().ToString()
122~
                })
123~
                .ToList();
124~

125~
            // Act
126~
            var tokensList = new List<Tokens>();
127~
            foreach (var user in users)
128~
            {
129~
                var tokens = _tokenService.GerarJwtToken(user, 60);
130~
                tokensList.Add(tokens);
131~
                
132~
                // Validar imediatamente
133~
                var principal = _tokenService.ValidarToken(tokens.Access_Token);
134~
                var email = _tokenService.ObterEmailToken(tokens.Access_Token);
135~
                
136~
                Assert.Equal(user.Email, email);
137~
            }
138~

139~
            // Assert - Todos os tokens devem ser únicos
140~
            var uniqueTokens = tokensList.Select(t => t.Access_Token).Distinct().Count();
141~
            Assert.Equal(users.Count, uniqueTokens);
142~
        }
143~

144~
        [Fact(DisplayName = "Deve lidar com expiração corretamente no fluxo completo")]
145~
        public void Deve_Lidar_Com_Expiracao_Fluxo_Completo()
146~
        {
147~
            // Arrange
148~
            var user = new UserViewModel
149~
            {
150~
                Email = _faker.Internet.Email(),
151~
                Stabilish_Id = _faker.Random.Guid().ToString()
152~
            };
153~

154~
            // Act - Criar token já expirado
155~
            var tokens = _tokenService.GerarJwtToken(user, -1);
156~
            System.Threading.Thread.Sleep(100);
157~

158~
            // Assert - Deve falhar sem ignorar expiração
159~
            Assert.Throws<Microsoft.IdentityModel.Tokens.SecurityTokenExpiredException>(() =>
160~
                _tokenService.ValidarToken(tokens.Access_Token, ignorarExpiracao: false));
161~

162~
            // Deve funcionar ignorando expiração
163~
            var principal = _tokenService.ValidarToken(tokens.Access_Token, ignorarExpiracao: true);
164~
            var email = _tokenService.ObterEmailToken(tokens.Access_Token);
165~

166~
            Assert.NotNull(principal);
167~
            Assert.Equal(user.Email, email);
168~
        }
169~

170~
        [Fact(DisplayName = "Deve gerar tokens com diferentes tempos de expiração funcionando corretamente")]
171~
        public void Deve_Gerar_Tokens_Diferentes_Expiracoes()
172~
        {
173~
            // Arrange
174~
            var user = new UserViewModel
175~
            {
176~
                Email = _faker.Internet.Email(),
177~
                Stabilish_Id = _faker.Random.Guid().ToString()
178~
            };
179~

180~
            // Act
181~
            var token1Min = _tokenService.GerarJwtToken(user, 1);
182~
            var token60Min = _tokenService.GerarJwtToken(user, 60);
183~
            var token1440Min = _tokenService.GerarJwtToken(user, 1440);
184~

185~
            // Assert
186~
            Assert.True(token1Min.ExpiryDate < token60Min.ExpiryDate);
187~
            Assert.True(token60Min.ExpiryDate < token1440Min.ExpiryDate);
188~

189~
            // Todos devem ser válidos
190~
            _tokenService.ValidarToken(token1Min.Access_Token);
191~
            _tokenService.ValidarToken(token60Min.Access_Token);
192~
            _tokenService.ValidarToken(token1440Min.Access_Token);
193~
        }
194~
    }
195~
}