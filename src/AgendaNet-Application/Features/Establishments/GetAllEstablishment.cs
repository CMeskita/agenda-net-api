using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;

namespace AgendaNet_Application.Features.Establishments
{
    public class GetAllEstablishment : IHandler<CommandGetAllEstablishment, ResponseEstablishment>
    {
        public Task<ResponseEstablishment> ExecuteAsync(CommandGetAllEstablishment request)
        {
            throw new NotImplementedException();
        }
    }
}
