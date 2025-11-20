using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using AgendaNet_Domain.Entities;
using AgendaNet_Domain.Interfaces;

namespace AgendaNet_Application.Features.Establishments
{
    public class GetIdEstablishmentHandler : IHandler<CommandGetIdEstablishment, ResponseEstablishment>
    {
        private readonly IUnitofWork _wow;

        public GetIdEstablishmentHandler(IUnitofWork wow)
        {
            _wow = wow;
        }

        public async Task<ResponseEstablishment> ExecuteAsync(CommandGetIdEstablishment request)
        {
            try
            {
               
               var establishment= await _wow.EstablishmentRepository.GetByIEstablishAsync(request.Id);

                return new ResponseEstablishment
                {
                    Name= establishment.Name,
                    Description= establishment.Description,
                    Email = establishment.Email,
                    Address = establishment.Address,
                    PhoneNumber = establishment.PhoneNumber,
                    ThemeColor= establishment.ThemeColor,
                    LogoUrl= establishment.LogoUrl,
                    StatusCode = 201,
                    Message = "Contato criado com sucesso.",
                    
                };

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
