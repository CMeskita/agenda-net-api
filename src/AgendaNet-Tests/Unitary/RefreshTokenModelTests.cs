using AgendaNet.Auth.Domain.Models;
using Bogus;

namespace AgendaNet_Tests.Unitary
{
    public class RefreshTokenModelTests
    {
        private readonly Faker _faker;

        public RefreshTokenModelTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve criar instância de RefreshToken")]
        public void Deve_Criar_Instancia_RefreshToken()
        {
            // Act
            var refreshToken = new RefreshToken();

            // Assert
            Assert.NotNull(refreshToken);
        }

        [Fact(DisplayName = "Deve definir e obter propriedade Token")]
        public void Deve_Definir_Obter_Token()
        {
            // Arrange
            var refreshToken = new RefreshToken();
            var token = _faker.Random.AlphaNumeric(64);

            // Act
            refreshToken.Token = token;

            // Assert
            Assert.Equal(token, refreshToken.Token);
        }

        [Fact(DisplayName = "Deve definir e obter propriedade ExpiryDate")]
        public void Deve_Definir_Obter_ExpiryDate()
        {
            // Arrange
            var refreshToken = new RefreshToken();
            var expiryDate = DateTime.UtcNow.AddDays(30);

            // Act
            refreshToken.ExpiryDate = expiryDate;

            // Assert
            Assert.Equal(expiryDate, refreshToken.ExpiryDate);
        }

        [Fact(DisplayName = "Deve criar RefreshToken completo")]
        public void Deve_Criar_RefreshToken_Completo()
        {
            // Arrange
            var token = _faker.Random.AlphaNumeric(64);
            var expiryDate = DateTime.UtcNow.AddDays(30);

            // Act
            var refreshToken = new RefreshToken
            {
                Token = token,
                ExpiryDate = expiryDate
            };

            // Assert
            Assert.Equal(token, refreshToken.Token);
            Assert.Equal(expiryDate, refreshToken.ExpiryDate);
        }

        [Theory(DisplayName = "Deve aceitar diferentes períodos de expiração")]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(30)]
        [InlineData(90)]
        public void Deve_Aceitar_Diferentes_Periodos_Expiracao(int dias)
        {
            // Arrange
            var refreshToken = new RefreshToken();
            var expiryDate = DateTime.UtcNow.AddDays(dias);

            // Act
            refreshToken.ExpiryDate = expiryDate;

            // Assert
            Assert.True(refreshToken.ExpiryDate > DateTime.UtcNow);
        }

        [Fact(DisplayName = "Deve permitir token nulo")]
        public void Deve_Permitir_Token_Nulo()
        {
            // Arrange
            var refreshToken = new RefreshToken();

            // Act
            refreshToken.Token = null;

            // Assert
            Assert.Null(refreshToken.Token);
        }
    }
}