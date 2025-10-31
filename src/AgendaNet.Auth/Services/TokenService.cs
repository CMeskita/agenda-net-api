using AgendaNet.Auth.Domain.Interfaces;
using AgendaNet.Auth.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgendaNet.Auth.Services
{
    public class TokenService: ITokenService
    {
 
        private readonly ILogger<TokenService> _logger;
        private readonly byte[] _keyToken;
  
        public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            var authKey = configuration["AUTHENTICATION"]
                     ?? throw new InvalidOperationException("A chave de autenticação não foi configurada.");


            if (authKey.Length < 32)
                throw new InvalidOperationException("A chave de autenticação deve ter pelo menos 32 caracteres.");

            _keyToken = Encoding.UTF8.GetBytes(authKey);

        }
        public Tokens GerarJwtToken(UserViewModel user, int time)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = CriarTokenDescriptor(user, time);
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var Acesstoken = tokenHandler.WriteToken(token);

                return new Tokens(
                    Acesstoken,
                    Guid.NewGuid().ToString().ToUpper(),
                    tokenDescriptor.Expires ?? DateTime.UtcNow
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o token para o usuário {Email}", user.Email);
                throw new InvalidOperationException("Erro ao gerar o token.", ex);
            }
        }

        public string ObterEmailToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token inválido ou vazio.", nameof(token));

            var principal = ValidarToken(token, ignorarExpiracao: true);
            var email = principal.FindFirstValue(JwtRegisteredClaimNames.Name);

            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidOperationException("O token não contém um e-mail válido.");

            return email;
        }

        public ClaimsPrincipal ValidarToken(string token, bool ignorarExpiracao = false)
        {

            var parametroDeValidacao = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = !ignorarExpiracao,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_keyToken),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, parametroDeValidacao, out var securityToken);

                if (securityToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Token inválido ou corrompido.");
                }

                return principal;
            }
            catch (SecurityTokenExpiredException)
            {
                if (ignorarExpiracao)
                    return tokenHandler.ValidateToken(token, parametroDeValidacao, out _);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha na validação do token JWT.");
                throw new SecurityTokenException("Falha na validação do token.", ex);
            }
        }
        private SecurityTokenDescriptor CriarTokenDescriptor(UserViewModel user, int tempoExpiracaoMinutos)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtRegisteredClaimNames.Name, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Sub, user.Stabilish_Id ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(tempoExpiracaoMinutos),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_keyToken),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        }
      
    }

}

