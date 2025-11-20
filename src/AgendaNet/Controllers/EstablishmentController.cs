using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgendaNet.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EstablishmentController : ControllerBase
    {
        private readonly MediatorService _mediator;
        public EstablishmentController(MediatorService mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Cadastro de Estabelicimento e vincula ao usuário, contatos e o tenant
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateEstablishment([FromBody] CommandEstablishment command)
        {
            var response = await _mediator.SendAsync<CommandEstablishment, Response>(command);
            return Ok(response);
        }
        /// <summary>
        /// Cadastra um segundo estabelecimento clonando o primeiro,sendo obrigatorio atualizar.
        /// </summary>
        /// <returns> retornand o tenant id</returns>
        /// <response code="200">Retorna  com sucesso.</response>
        [HttpPost("store")]
        public async Task<IActionResult> DuplicaredEstablishment([FromBody] CommandDuplicateEstablishment command)
        {
            var response = await _mediator.SendAsync<CommandDuplicateEstablishment, Response>(command);
            return Ok(response);
        }
        /// <summary>
        /// Cadastra um documento vinculado ao estabelecimento/usuário.
        /// </summary>
        /// <returns> retornana o Estabelecimento id</returns>
        /// <response code="200">Retorna  com sucesso.</response>
        [HttpPost("document")]
        public async Task<IActionResult> CreateDocumentEstablishment([FromBody] CommanEstablishmentDcoument command)
        {
            var response = await _mediator.SendAsync<CommanEstablishmentDcoument, Response>(command);
            return Ok(response);
        }
        /// <summary>
        /// Cadastra um usuário vinculado ao estabelecimento.
        /// </summary>
        /// <returns> retornana o Estabelecimento id</returns>
        /// <response code="200">Retorna  com sucesso.</response>

        [HttpPost("user")]
        public async Task<IActionResult> CreateUserEstablishment([FromBody] CommandEstablishmentUser command)
        {
            var response = await _mediator.SendAsync<CommandEstablishmentUser, Response>(command);
            return Ok(response);
        }
        /// <summary>
        /// Cadastra um contato vinculado ao usuário/estabelecimento.
        /// </summary>
        /// <returns> retornana o usuario id</returns>
        /// <response code="200">Retorna  com sucesso.</response>
        [HttpPost("contact")]
        public async Task<IActionResult> CreateContactEstablishment([FromBody] CommandEstablishmentUserContact command)
        {
            var response = await _mediator.SendAsync<CommandEstablishmentUserContact, Response>(command);
            return Ok(response);
        }
        /// <summary>
        /// Lista todos os estabelecimento vinculado ao Tenant.
        /// </summary>
        /// <returns> retornana o Estabelecimento id</returns>
        /// <response code="200">Retorna  com sucesso.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllEstablishments([FromQuery] CommandGetAllEstablishment command)
        {
            
            var response = await _mediator.SendAsync< CommandGetAllEstablishment, List<ResponseGetallEstablishment>> (command);
            return Ok(response);
        }
        /// <summary>
        /// Mostra informações do estabelecimento buscando pelo Id
        /// </summary>
        /// <returns> retornana o Estabelecimento id</returns>
        /// <response code="200">Retorna  com sucesso.</response>
        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetIDEstablishments([FromQuery] CommandGetIdEstablishment command)
        {

            var response = await _mediator.SendAsync<CommandGetIdEstablishment, ResponseEstablishment>(command);
            return Ok(response);
        }
        /// <summary>
        /// Lista todos os tenants cadastrados e seus estabelecimentos.
        /// </summary>
        /// <returns> retornana o Estabelecimento id</returns>
        /// <response code="200">Retorna  com sucesso.</response>
        [HttpGet]
        [Route("tenants")]
        public async Task<IActionResult> GetAllEstablishmentsTenants([FromQuery] CommandGetAllTenants command)
        {

            var response = await _mediator.SendAsync<CommandGetAllTenants, ResponseGetallTenant>(command);
            return Ok(response);
        }
    }
}
