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
    public class CreateUserEstablishmentHandler : IHandler<CommandEstablishmentUser, Response>
    {
        private readonly IUnitofWork _wow;

        public CreateUserEstablishmentHandler(IUnitofWork wow)
        {
            _wow = wow;
        }

        public async Task<Response> ExecuteAsync(CommandEstablishmentUser request)
        {
            try
            {
                User data = request;

                _wow.BeginTransaction();
                await _wow.UserRepository.SaveAsync(data);

                return new Response
                {
                    StatusCode = 201,
                    Message = "Usuário criado com sucesso,.",
                };

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
