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
    public class CreateDocumentEstablishmentHandler : IHandler<CommanEstablishmentDcoument, Response>
    {
        private readonly IUnitofWork _wow;

        public CreateDocumentEstablishmentHandler(IUnitofWork wow)
        {
            _wow = wow;
        }

        public async Task<Response> ExecuteAsync(CommanEstablishmentDcoument request)
        {
            try
            {
                Document data = request;

                _wow.BeginTransaction();
                await _wow.EstablishmentRepository.SaveDocumentAsync(data);

                return new Response
                {
                    StatusCode = 201,
                    Message = "Documento criado com sucesso, vinculado ao Estabelecimento:.",
                    Detalhe = data.EstablishmentId
                };

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
