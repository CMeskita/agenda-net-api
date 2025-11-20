using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using AgendaNet_Domain.Entities;
using AgendaNet_Domain.Interfaces;
using System.Collections.Generic;

namespace AgendaNet_Application.Features.Establishments
{
    public class GetAllEstablishmentHandler : IHandler<CommandGetAllEstablishment, List<ResponseGetallEstablishment>>
    {
        private readonly IUnitofWork _wow;

        public GetAllEstablishmentHandler(IUnitofWork wow)
        {
            _wow = wow;
        }

        public async Task<List<ResponseGetallEstablishment>>ExecuteAsync(CommandGetAllEstablishment request)
        {
            try
            {

                IEnumerable <EstablishmentTenant> estabelishmentTenant = await _wow.EstablishmentRepository.GetAllIEstablishmentByTenantaAsync(request.EstablishmentTenantId);

                List<ResponseGetallEstablishment> listarEstablishment = new List<ResponseGetallEstablishment>();

                foreach (var item in estabelishmentTenant)
                {
                    var estabelishment= await _wow.EstablishmentRepository.GetByIEstablishAsync(item.EstablishmentId);
                    listarEstablishment.Add(new ResponseGetallEstablishment
                    {
                        Dados = new List<ResponseEstablishment>
                        {
                            new ResponseEstablishment
                            {
                                Name = estabelishment.Name,
                                Description = estabelishment.Description,
                                Address = estabelishment.Address,
                                PhoneNumber = estabelishment.PhoneNumber,
                                Email = estabelishment.Email,
                                ThemeColor = estabelishment.ThemeColor,
                                LogoUrl = estabelishment.LogoUrl,
                                itemStore=item.ItemStore
                            }
                        }
                    });
                }
                return listarEstablishment;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
