using AgendaNet.Auth.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace AgendaNet_Tests.Unitary
{
    public class TokenServiceConstructorTests
    {
        [Fact(DisplayName = "Deve criar instância com configuração válida")]
        public void Deve_Criar_Instancia_Com_Configuracao_Valida()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", "testkey_testkey_testkey_testkey_testkey_testkey_testkey_testkey_" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act
            var tokenService = new TokenService(configuration, new NullLogger<TokenService>());

            // Assert
            Assert.NotNull(tokenService);
        }

        [Fact(DisplayName = "Deve lançar ArgumentNullException quando logger for nulo")]
        public void Deve_Lancar_ArgumentNullException_Quando_Logger_Nulo()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", "testkey_testkey_testkey_testkey_testkey_testkey_testkey_testkey_" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TokenService(configuration, null));
        }

        [Fact(DisplayName = "Deve lançar InvalidOperationException quando chave de autenticação não estiver configurada")]
        public void Deve_Lancar_InvalidOperationException_Quando_Chave_Nao_Configurada()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string>();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                new TokenService(configuration, new NullLogger<TokenService>()));

            Assert.Contains("autenticação não foi configurada", exception.Message);
        }

        [Fact(DisplayName = "Deve lançar InvalidOperationException quando chave for muito curta")]
        public void Deve_Lancar_InvalidOperationException_Quando_Chave_Curta()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", "chavemuito_curta" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                new TokenService(configuration, new NullLogger<TokenService>()));

            Assert.Contains("32 caracteres", exception.Message);
        }

        [Fact(DisplayName = "Deve aceitar chave com caracteres especiais")]
        public void Deve_Aceitar_Chave_Com_Caracteres_Especiais()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", "!@#$%^&*()_+-=[]{}|;:',.<>?/~`12345678901234567890" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act
            var tokenService = new TokenService(configuration, new NullLogger<TokenService>());

            // Assert
            Assert.NotNull(tokenService);
        }

        [Fact(DisplayName = "Deve aceitar chave hexadecimal")]
        public void Deve_Aceitar_Chave_Hexadecimal()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", "testkey_testkey_testkey_testkey_" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act
            var tokenService = new TokenService(configuration, new NullLogger<TokenService>());

            // Assert
            Assert.NotNull(tokenService);
        }

        [Fact(DisplayName = "Deve lidar com chave contendo espaços")]
        public void Deve_Aceitar_Chave_Com_Espacos()
        {
            // Arrange
            var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", "chave com espaços e mais caracteres para completar 32" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act
            var tokenService = new TokenService(configuration, new NullLogger<TokenService>());

            // Assert
            Assert.NotNull(tokenService);
        }
    }
}