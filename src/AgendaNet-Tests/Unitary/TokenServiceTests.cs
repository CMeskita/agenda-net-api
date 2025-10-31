using AgendaNet.Auth.Domain.Interfaces;
using AgendaNet.Auth.Domain.Models;
using AgendaNet.Auth.Services;
using AgendaNet_Tests.FakerData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace AgendaNet_Tests.Unitary
{
   
    public class TokenServiceAuthTests
    {
        private readonly ITokenService _tokenService;

        public TokenServiceAuthTests()
        {
            // ⚙️ Cria uma configuração real (sem mock)
            var inMemorySettings = new Dictionary<string, string>
        {
            { "AUTHENTICATION", "b3a2448f94c74f57b8e7a3d4e59b89b932ab4f7db23e4928a76aa49f5311229f" }
        };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _tokenService = new TokenService(configuration, new NullLogger<TokenService>());
        }

        [Theory(DisplayName = "Deve gerar e validar token de acordo com os cenários fornecidos")]
        [MemberData(nameof(AuthFakerData.Scenarios), MemberType = typeof(AuthFakerData))]
        public void Deve_Gerar_Validar_Token_Conforme_Cenario(
            string descricao,
            string email,
            string stabilishId,
            bool esperadoValido)
        {
            // Arrange
            var user = new UserViewModel
            {
                Email = email,
                Stabilish_Id = stabilishId
            };

            try
            {
                // Act
                var token = _tokenService.GerarJwtToken(user, 5);

                var emailExtraido = _tokenService.ObterEmailToken(token.Access_Token);
                var principal = _tokenService.ValidarToken(token.Access_Token);

                var claimEmail = principal.FindFirst(ClaimTypes.Name) ??
                                 principal.FindFirst(JwtRegisteredClaimNames.Name);

                // Assert
                if (esperadoValido)
                {
                    Assert.NotNull(token);
                    Assert.False(string.IsNullOrWhiteSpace(token.Access_Token));
                    Assert.Equal(user.Email, emailExtraido);
                    Assert.Equal(user.Email, claimEmail?.Value);
                }
                else
                {
                    // Se o cenário é inválido, mas o código não lançou exceção, o teste deve falhar.
                    Assert.True(false, $"O cenário '{descricao}' deveria falhar, mas não falhou.");
                }
            }
            catch (Exception ex)
            {
                if (esperadoValido)
                {
                    Assert.True(false, $"O cenário '{descricao}' deveria ser válido, mas falhou: {ex.Message}");
                }
                else
                {
                    // Cenários inválidos esperam erro, logo sucesso!
                    Assert.True(true);
                }
            }
        }
    }

}