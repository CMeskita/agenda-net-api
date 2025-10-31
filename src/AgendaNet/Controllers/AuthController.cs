using AgendaNet.Auth.Domain.Interfaces;
using AgendaNet.Auth.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaNet.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _mailService;

        public AuthController(ITokenService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel user)
        {
            var response = _mailService.GerarJwtToken(user, 60);
            return Ok(response);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserViewModel user)
        {
            var session = await GetTokenHeader();
            var email = _mailService.ObterEmailToken(session);
            if (email != user.Email || session == null)
            {

                return NotFound("Falha na Authenticação");
            }


            return Ok("Login Autenticado com o Email :" + email);
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
