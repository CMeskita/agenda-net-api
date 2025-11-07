
using AgendaNet.Auth.Domain.Models;
using AgendaNet.Auth.Services;
using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
namespace AgendaNet_Tests.Integration
{
        public class TokenServiceIntegrationTests
        {

                private readonly TokenService _tokenService;

                private readonly Faker _faker;

                public TokenServiceIntegrationTests()
                {
                        var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", "b3a2448f94c74f57b8e7a3d4e59b89b932ab4f7db23e4928a76aa49f5311229f" }

            };
                        IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
                        _tokenService = new TokenService(configuration, new NullLogger<TokenService>());
                        _faker = new Faker("pt_BR");

                }

                [Fact(DisplayName = "Deve executar fluxo completo de geração, validação e extração de email")]
                public void Deve_Executar_Fluxo_Completo()
                {
                        // Arrange
                        var user = new UserViewModel
                        {
                                Email = _faker.Internet.Email(),
                                Stabilish_Id = _faker.Random.Guid().ToString(),
                                UserName = _faker.Internet.UserName(),
                                Password = _faker.Internet.Password()
                        };

                        // Act - Gerar token
                        var tokens = _tokenService.GerarJwtToken(user, 60);

                        // Act - Validar token

                        var principal = _tokenService.ValidarToken(tokens.Access_Token);
                        // Act - Extrair email

                        var emailExtraido = _tokenService.ObterEmailToken(tokens.Access_Token);

                        // Assert
                        Assert.NotNull(tokens);
                        Assert.NotNull(tokens.Access_Token);
                        Assert.NotNull(principal);
                        Assert.Equal(user.Email, emailExtraido);
                }

                [Fact(DisplayName = "Deve gerar múltiplos tokens independentes para o mesmo usuário")]
                public void Deve_Gerar_Multiplos_Tokens_Independentes()
                {
                        // Arrange
                        var user = new UserViewModel
                        {
                                Email = _faker.Internet.Email(),
                                Stabilish_Id = _faker.Random.Guid().ToString()
                        };
                        // Act

                        var tokens1 = _tokenService.GerarJwtToken(user, 30);
                        var tokens2 = _tokenService.GerarJwtToken(user, 30);
                        var tokens3 = _tokenService.GerarJwtToken(user, 30);

                        // Assert
                        Assert.NotEqual(tokens1.Access_Token, tokens2.Access_Token);
                        Assert.NotEqual(tokens2.Access_Token, tokens3.Access_Token);
                        Assert.NotEqual(tokens1.JwtId, tokens2.JwtId);

                        // Todos devem ser válidos independentemente
                        var principal1 = _tokenService.ValidarToken(tokens1.Access_Token);
                        var principal2 = _tokenService.ValidarToken(tokens2.Access_Token);
                        var principal3 = _tokenService.ValidarToken(tokens3.Access_Token);

                        Assert.NotNull(principal1);
                        Assert.NotNull(principal2);
                        Assert.NotNull(principal3);
                }
                [Fact(DisplayName = "Deve processar sequencialmente múltiplas operações")]
                public void Deve_Processar_Sequencialmente_Multiplas_Operacoes()
                {
                        // Arrange
                        var users = Enumerable.Range(0, 10)
                            .Select(i => new UserViewModel
                            {
                                    Email = _faker.Internet.Email(),
                                    Stabilish_Id = _faker.Random.Guid().ToString()
                            }).ToList();

                        // Act

                        var tokensList = new List<Tokens>();
                        foreach (var user in users)
                        {
                                var tokens = _tokenService.GerarJwtToken(user, 60);
                                tokensList.Add(tokens);

                                // Validar imediatamente
                                var principal = _tokenService.ValidarToken(tokens.Access_Token);
                                var email = _tokenService.ObterEmailToken(tokens.Access_Token);
                                Assert.Equal(user.Email, email);
                        }
                        // Assert - Todos os tokens devem ser únicos
                        var uniqueTokens = tokensList.Select(t => t.Access_Token).Distinct().Count();
                        Assert.Equal(users.Count, uniqueTokens);
                }
                [Fact(DisplayName = "Deve gerar tokens com diferentes tempos de expiração funcionando corretamente")]
                public void Deve_Gerar_Tokens_Diferentes_Expiracoes()
                {
                        // Arrange
                        var user = new UserViewModel
                        {
                                Email = _faker.Internet.Email(),
                                Stabilish_Id = _faker.Random.Guid().ToString()
                        };

                        // Act
                        var token1Min = _tokenService.GerarJwtToken(user, 1);
                        var token60Min = _tokenService.GerarJwtToken(user, 60);
                        var token1440Min = _tokenService.GerarJwtToken(user, 1440);
                        // Assert
                        Assert.True(token1Min.ExpiryDate < token60Min.ExpiryDate);
                        Assert.True(token60Min.ExpiryDate < token1440Min.ExpiryDate);

                        // Todos devem ser válidos
                        _tokenService.ValidarToken(token1Min.Access_Token);
                        _tokenService.ValidarToken(token60Min.Access_Token);
                        _tokenService.ValidarToken(token1440Min.Access_Token);
                }
        } 
}
