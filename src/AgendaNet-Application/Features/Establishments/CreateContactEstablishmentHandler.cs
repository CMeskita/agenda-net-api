using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using AgendaNet_Domain.Entities;
using AgendaNet_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaNet_Application.Features.Establishments
{
    internal class CreateContactEstablishmentHandler : IHandler<CommandEstablishmentUserContact, Response>
    {
        private readonly IUnitofWork _wow;

        public CreateContactEstablishmentHandler(IUnitofWork wow)
        {
            _wow = wow;
        }

        public async Task<Response> ExecuteAsync(CommandEstablishmentUserContact request)
        {

            try
            {
                Contact data = request;

                _wow.BeginTransaction();
               await _wow.UserRepository.SaveContactAsync(data);

                return new Response
                {
                    StatusCode = 201,
                    Message = "Contato criado com sucesso vinculado ao usuario:.",
                    Detalhe = data.UserId
                };

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
