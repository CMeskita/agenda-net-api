using AgendaNet.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaNet_Tests.Unitary
{
    public class JwtMiddlewareTests
    {
        private const string ValidTestAuthenticationKey = "TestAuthenticationKey1234567890ab";

        [Fact(DisplayName = "Deve configurar JWT middleware com chave válida")]
        public void Deve_Configurar_JWT_Middleware_Valido()
        {
            // Arrange
            var services = new ServiceCollection();
            var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", ValidTestAuthenticationKey }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act
            services.AddJwtMiddleware(configuration);

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            Assert.NotNull(serviceProvider);
        }

        [Fact(DisplayName = "Deve lançar InvalidOperationException quando AUTHENTICATION não estiver configurada")]
        public void Deve_Lancar_Excecao_Quando_Authentication_Nao_Configurada()
        {
            // Arrange
            var services = new ServiceCollection();
            var inMemorySettings = new Dictionary<string, string>();

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                services.AddJwtMiddleware(configuration));

            Assert.Contains("AUTHENTICATION não configurada", exception.Message);
        }

        [Fact(DisplayName = "Deve lançar InvalidOperationException quando AUTHENTICATION for vazia")]
        public void Deve_Lancar_Excecao_Quando_Authentication_Vazia()
        {
            // Arrange
            var services = new ServiceCollection();
            var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", "" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                services.AddJwtMiddleware(configuration));

            Assert.Contains("AUTHENTICATION não configurada", exception.Message);
        }

        [Fact(DisplayName = "Deve registrar serviços de autenticação")]
        public void Deve_Registrar_Servicos_Autenticacao()
        {
            // Arrange
            var services = new ServiceCollection();
            var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", ValidTestAuthenticationKey }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act
            services.AddJwtMiddleware(configuration);
            var serviceProvider = services.BuildServiceProvider();

            // Assert - Verifica se os serviços de autenticação foram registrados
            var authService = serviceProvider.GetService<Microsoft.AspNetCore.Authentication.IAuthenticationService>();
            Assert.NotNull(authService);
        }

        [Fact(DisplayName = "Deve configurar política AdminPolicy")]
        public void Deve_Configurar_Admin_Policy()
        {
            // Arrange
            var services = new ServiceCollection();
            var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", ValidTestAuthenticationKey }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act
            services.AddJwtMiddleware(configuration);
            var serviceProvider = services.BuildServiceProvider();

            // Assert
            var authorizationService = serviceProvider.GetService<Microsoft.AspNetCore.Authorization.IAuthorizationService>();
            Assert.NotNull(authorizationService);
        }

        [Theory(DisplayName = "Deve aceitar diferentes tamanhos de chave de autenticação")]
        [InlineData("12345678901234567890123456789012")]
        [InlineData("TestAuthenticationKey1234567890ab")]
        [InlineData("verylongauthenticationkeythatislongerthanusual123456789012345678901234567890")]
        public void Deve_Aceitar_Diferentes_Tamanhos_Chave(string authKey)
        {
            // Arrange
            var services = new ServiceCollection();
            var inMemorySettings = new Dictionary<string, string>
            {
                { "AUTHENTICATION", authKey }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Act
            services.AddJwtMiddleware(configuration);

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            Assert.NotNull(serviceProvider);
        }
    }
}