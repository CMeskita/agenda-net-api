using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using AgendaNet_Domain.Entities;
using AgendaNet_Domain.Interfaces;

namespace AgendaNet_Application.Features.Establishments
{
    public class DuplicatedEstablishmentHandler : IHandler<CommandDuplicateEstablishment, Response>
    {
        private readonly IUnitofWork _wow;

        public DuplicatedEstablishmentHandler(IUnitofWork wow)
        {
            _wow = wow;
        }

        public async Task<Response> ExecuteAsync(CommandDuplicateEstablishment request)
        {

            try
            {
                

                _wow.BeginTransaction();

                // Verificar se existe um estabelecimento com o mesmo e-mail
                var anyEstablishment = await _wow.EstablishmentRepository.GetEstablishmentTenantMaxAsync(request.TenantId);
                if (anyEstablishment == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Não é possivel cadastara fiflial, pois não tem loja matriz."
                    };
                 
                }

                var establishmentTenantMax = await _wow.EstablishmentRepository.GetByIEstablishUnicAsync(anyEstablishment.EstablishmentId);
                // Criar o estabelecimento

                Establishment data = establishmentTenantMax;
                data.SetGuidId();
                data.Deactivate();
                var establishment = await _wow.EstablishmentRepository.SaveAsync(data);

                // Criar tenant
         
                var tenant = new EstablishmentTenant(anyEstablishment.Id, establishment.Id, establishment.Email, establishment);
                await _wow.EstablishmentRepository.TenantSaveAsync(tenant);

                // Usuário vinculado
              
                var user = new User(establishment.Name, establishment.Email, "establish", establishment.Id);
                user.setAcessed(true);
                var savedUser = await _wow.UserRepository.SaveAsync(user);

                // Contato
                var contact = new Contact("Email", savedUser.Email, savedUser.Id);
                await _wow.UserRepository.SaveContactAsync(contact);

                await _wow.CommitAsync();
                _wow.CommitTransaction();

                return new Response
                {
                    StatusCode = 201,
                    Message = "Estabelecimento criado com sucesso."
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
