using AgendaNet.Auth.Domain.Models;
using Bogus;

namespace AgendaNet_Tests.Unitary
{
    public class TokensModelTests
    {
        private readonly Faker _faker;

        public TokensModelTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact(DisplayName = "Deve criar instância de Tokens com valores válidos")]
        public void Deve_Criar_Instancia_Tokens_Validos()
        {
            // Arrange
            var accessToken = _faker.Random.AlphaNumeric(100);
            var jwtId = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddHours(1);

            // Act
            var tokens = new Tokens(accessToken, jwtId, expiryDate);

            // Assert
            Assert.Equal(accessToken, tokens.Access_Token);
            Assert.Equal(jwtId, tokens.JwtId);
            Assert.Equal(expiryDate, tokens.ExpiryDate);
            Assert.False(tokens.IsUsed);
            Assert.False(tokens.IsRevorked);
            Assert.True(tokens.AddedDate <= DateTime.UtcNow);
            Assert.True(tokens.AddedDate >= DateTime.UtcNow.AddSeconds(-5));
        }

        [Fact(DisplayName = "Deve lançar ArgumentNullException quando accessToken for nulo")]
        public void Deve_Lancar_ArgumentNullException_AccessToken_Nulo()
        {
            // Arrange
            var jwtId = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddHours(1);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new Tokens(null, jwtId, expiryDate));
        }

        [Fact(DisplayName = "Deve lançar ArgumentNullException quando jwtId for nulo")]
        public void Deve_Lancar_ArgumentNullException_JwtId_Nulo()
        {
            // Arrange
            var accessToken = _faker.Random.AlphaNumeric(100);
            var expiryDate = DateTime.UtcNow.AddHours(1);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new Tokens(accessToken, null, expiryDate));
        }

        [Fact(DisplayName = "Deve inicializar IsUsed como false")]
        public void Deve_Inicializar_IsUsed_False()
        {
            // Arrange
            var accessToken = _faker.Random.AlphaNumeric(100);
            var jwtId = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddHours(1);

            // Act
            var tokens = new Tokens(accessToken, jwtId, expiryDate);

            // Assert
            Assert.False(tokens.IsUsed);
        }

        [Fact(DisplayName = "Deve inicializar IsRevorked como false")]
        public void Deve_Inicializar_IsRevorked_False()
        {
            // Arrange
            var accessToken = _faker.Random.AlphaNumeric(100);
            var jwtId = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddHours(1);

            // Act
            var tokens = new Tokens(accessToken, jwtId, expiryDate);

            // Assert
            Assert.False(tokens.IsRevorked);
        }

        [Fact(DisplayName = "Deve definir AddedDate próximo ao momento da criação")]
        public void Deve_Definir_AddedDate_Correto()
        {
            // Arrange
            var antes = DateTime.UtcNow;
            var accessToken = _faker.Random.AlphaNumeric(100);
            var jwtId = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddHours(1);

            // Act
            var tokens = new Tokens(accessToken, jwtId, expiryDate);
            var depois = DateTime.UtcNow;

            // Assert
            Assert.True(tokens.AddedDate >= antes);
            Assert.True(tokens.AddedDate <= depois);
        }

        [Theory(DisplayName = "Deve aceitar diferentes formatos de data de expiração")]
        [InlineData(1)]
        [InlineData(24)]
        [InlineData(168)]
        [InlineData(720)]
        public void Deve_Aceitar_Diferentes_Datas_Expiracao(int horas)
        {
            // Arrange
            var accessToken = _faker.Random.AlphaNumeric(100);
            var jwtId = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddHours(horas);

            // Act
            var tokens = new Tokens(accessToken, jwtId, expiryDate);

            // Assert
            Assert.Equal(expiryDate, tokens.ExpiryDate);
        }

        [Fact(DisplayName = "Deve aceitar data de expiração no passado")]
        public void Deve_Aceitar_Data_Expiracao_Passado()
        {
            // Arrange
            var accessToken = _faker.Random.AlphaNumeric(100);
            var jwtId = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddHours(-1);

            // Act
            var tokens = new Tokens(accessToken, jwtId, expiryDate);

            // Assert
            Assert.Equal(expiryDate, tokens.ExpiryDate);
            Assert.True(tokens.ExpiryDate < DateTime.UtcNow);
        }

        [Fact(DisplayName = "Deve permitir modificação de IsUsed")]
        public void Deve_Permitir_Modificacao_IsUsed()
        {
            // Arrange
            var accessToken = _faker.Random.AlphaNumeric(100);
            var jwtId = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddHours(1);
            var tokens = new Tokens(accessToken, jwtId, expiryDate);

            // Act
            tokens.IsUsed = true;

            // Assert
            Assert.True(tokens.IsUsed);
        }

        [Fact(DisplayName = "Deve permitir modificação de IsRevorked")]
        public void Deve_Permitir_Modificacao_IsRevorked()
        {
            // Arrange
            var accessToken = _faker.Random.AlphaNumeric(100);
            var jwtId = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddHours(1);
            var tokens = new Tokens(accessToken, jwtId, expiryDate);

            // Act
            tokens.IsRevorked = true;

            // Assert
            Assert.True(tokens.IsRevorked);
        }
    }
}