using AgendaNet_email.Domain.Interfaces;
using AgendaNet_email.Domain.Models;
using AgendaNet_email.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace AgendaNet.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public SendMailController(MailService mailService)
        {
            _mailService = mailService;

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MailViewModel mailViewModel)
        {


            try
            {

                _mailService.SendEmail(mailViewModel.Emails, mailViewModel.Subject, mailViewModel.Body, mailViewModel.IsHtml);
                return Ok("Email enviado com sucesso");
            }
            catch (Exception ex)
            {
                return Ok($"Erro ao tentar enviar o e-mail {ex.Message}");
            }
        }
    }
}
