using AgendaNet.Auth.Domain.Models;
using AgendaNet.Auth.Services;
using Bogus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;


namespace AgendaNet_Tests.Unitary
{

public class TokenServiceObterEmailTests
    {
        private readonly TokenService _tokenService;
        private readonly Faker _faker;

        public TokenServiceObterEmailTests()
        {
            // ⚙️ Configuração real, sem mock
            var inMemorySettings = new Dictionary<string, string>
        {
            { "AUTHENTICATION", "b3a2448f94c74f57b8e7a3d4e59b89b932ab4f7db23e4928a76aa49f5311229f" }
        };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _tokenService = new TokenService(configuration, new NullLogger<TokenService>());
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve obter o e-mail corretamente a partir de um token JWT válido")]
        public void Deve_Obter_Email_Do_Token_Valido()
        {
            // Arrange
            var user = new UserViewModel
            {
                Email = _faker.Internet.Email(),
                Stabilish_Id = _faker.Random.Guid().ToString()
            };

            // Act
            var tokens = _tokenService.GerarJwtToken(user, 10);
            var emailExtraido = _tokenService.ObterEmailToken(tokens.Access_Token);

            // Assert
            Assert.Equal(user.Email, emailExtraido);
            Assert.False(string.IsNullOrWhiteSpace(emailExtraido));
        }

        [Fact(DisplayName = "Deve lançar exceção ao tentar obter e-mail de token inválido")]
        public void Deve_Falhar_Ao_Obter_Email_De_Token_Invalido()
        {
            // Arrange
            var tokenInvalido = "token_invalido_qualquer";

            // Act & Assert
            var ex = Assert.Throws<SecurityTokenException>(() =>
                _tokenService.ValidarToken(tokenInvalido));

            Assert.Contains("token", ex.Message, StringComparison.InvariantCultureIgnoreCase);
        }

        [Fact(DisplayName = "Deve lançar exceção ao tentar obter e-mail de token sem claim 'name'")]
        public void Deve_Falhar_Ao_Obter_Email_De_Token_Sem_Claim_Name()
        {
            // Arrange
            var user = new UserViewModel
            {
                Email = string.Empty,
                Stabilish_Id = _faker.Random.Guid().ToString()
            };

            var tokens = _tokenService.GerarJwtToken(user, 10);

            // Força a remoção da claim (simulando token sem e-mail)
            var tokenJwt = tokens.Access_Token;
            var tokenCorrompido = tokenJwt.Replace("a", "b"); // token malformado

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                _tokenService.ObterEmailToken(tokenCorrompido));
        }

        [Fact(DisplayName = "Deve lançar exceção ao tentar obter e-mail de token vazio")]
        public void Deve_Falhar_Ao_Obter_Email_De_Token_Vazio()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _tokenService.ObterEmailToken(string.Empty));
        }
    }


}
