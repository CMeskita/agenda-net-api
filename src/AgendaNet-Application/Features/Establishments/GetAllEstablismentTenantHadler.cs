using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using AgendaNet_Domain.Interfaces;

namespace AgendaNet_Application.Features.Establishments
{
    public class GetAllEstablismentTenantHadler : IHandler<CommandGetAllTenants, ResponseGetallTenant>
    {
        private readonly IUnitofWork _wow;

        public GetAllEstablismentTenantHadler(IUnitofWork wow)
        {
            _wow = wow;
        }

        public async Task<ResponseGetallTenant> ExecuteAsync(CommandGetAllTenants request)
        {

            try
            {

                var estabelishmentTenant = await _wow.EstablishmentRepository.GetAllEstablishmentTeantAsync();

                ResponseGetallTenant response = new ResponseGetallTenant
                {
                    Dados = estabelishmentTenant.Select(et => new ResponseTenant
                    {
                       Id= et.Id,
                       EstablishmentId= et.EstablishmentId,

                    }).ToList()
                };


                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
