using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaNet_Application.Features.Users
{
    public class ResetPasswordUserEstablishmentHandler : IHandler<CommandUsers, ResponseToken>
    {
        public Task<ResponseToken> ExecuteAsync(CommandUsers request)
        {
            throw new NotImplementedException();
        }
    }
}
