using AgendaNet_Application.Commands;
using AgendaNet_Application.Core;
using AgendaNet_Application.Responses;
using Microsoft.AspNetCore.Mvc;


namespace AgendaNet.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly MediatorService _mediator;

        public AuthenticationController(MediatorService mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Faz registro de usuários.
        /// </summary>
        /// <returns>Uma lista de previsões do tempo.</returns>
        /// <response code="200">Retorna a lista de previsões com sucesso.</response>
        //[HttpPost]
        //[Route("Register")]
        //public async Task<IActionResult> Register([FromBody] UserViewModel user)
        //{
   

        //    // var response = _mailService.GerarJwtToken(user, 60);
        //    return Ok();
        //}
        /// <summary>
        /// Faz o login validando email e senha.
        /// </summary>
        /// <returns>Um Toke de Acesso</returns>
        /// <response code="200">Retorna  com sucesso.</response>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] CommandUsers command)
        {

            var response = await _mediator.SendAsync<CommandUsers, ResponseToken>(command);
            return Ok(response);

        }
        /// <summary>
        /// Faz resete de senha ao criar o primeiro Estabelecimento.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna  com sucesso.</response>
        [HttpPut]
        [Route("first-login")]
        public async Task<IActionResult> FirstLogin([FromBody] CommandResetLogrinUsers command)
        {

            var response = await _mediator.SendAsync<CommandResetLogrinUsers, Response>(command);
            return Ok(response);

        }
        [HttpGet]
        [Route("Token-header")]
        public async Task<string> GetTokenHeader()
        {
            string authorizationHeader = HttpContext.Request.Headers["Authorization"];
            var session = "";
            {
                if (authorizationHeader != null)
                {
                    session = authorizationHeader.Substring(7);
                }
                else
                {

                    return "Informe o Token";
                }

                return session;
            }
        }

    }
}
