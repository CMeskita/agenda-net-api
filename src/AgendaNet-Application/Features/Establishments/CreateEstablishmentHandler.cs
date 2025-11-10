using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using AgendaNet_Domain.Entities;
using AgendaNet_Domain.Interfaces;
namespace AgendaNet_Application.Features.Establishments
{
    public class CreateEstablishmentHandler : IHandler<CommandEstablishment, Response>
    {
        private readonly IEstablishmentRepository _repository;
        private readonly IUnitofWork _wow;

        public CreateEstablishmentHandler(IEstablishmentRepository repository, IUnitofWork wow)
        {
            _repository = repository;
            _wow = wow;
        }

        public async Task<Response> ExecuteAsync(CommandEstablishment request)
        {
            try
            {
                Establishment data = request;
                _wow.BeginTransaction();
                   await _repository.SaveAsync(data);

                _wow.CommitTransaction();
                return new Response
                {
                    StatusCode = 201,
                    Message = "Estabelecimento criado com sucesso.",
               
                };

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
