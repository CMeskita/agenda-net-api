using AgendaNet.Auth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgendaNet.Auth.Domain.Interfaces
{
    public interface ITokenService
    {
        Tokens GerarJwtToken(UserViewModel user, int tempoExpiracaoMinutos);
        string ObterEmailToken(string token);
        ClaimsPrincipal ValidarToken(string token, bool ignorarExpiracao = false);
    }
}
