using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AgendaNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablishmentController : ControllerBase
    {
        private readonly MediatorService _mediator;
        public EstablishmentController(MediatorService mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateEstablishment([FromBody] CommandEstablishment command)
        {
            var response = await _mediator.SendAsync<CommandEstablishment, Response>(command);
            return Ok(response);
        }
        [HttpPost("document")]
        public async Task<IActionResult> CreateDocument([FromBody] CommandEstablishment command)
        {
            var response = await _mediator.SendAsync<CommandEstablishment, Response>(command);
            return Ok(response);
        }
        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] CommandEstablishment command)
        {
            var response = await _mediator.SendAsync<CommandEstablishment,Response>(command);
            return Ok(response);
        }
        [HttpPost("contact")]
        public async Task<IActionResult> CreateContact([FromBody] CommandEstablishment command)
        {
            var response = await _mediator.SendAsync<CommandEstablishment, Response>(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllEstablishments([FromQuery] CommandGetAllEstablishment command)
        {
            
            var response = await _mediator.SendAsync<CommandGetAllEstablishment, ResponseEstablishment>(command);
            return Ok(response);
        }
    }
}
