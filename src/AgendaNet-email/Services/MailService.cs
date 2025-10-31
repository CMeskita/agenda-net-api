using AgendaNet_email.Domain.Interfaces;
using System.Net.Mail;

namespace AgendaNet_email.Services
{
    public class MailService : IMailService
    {
        private string smtpServer = "smtp.gmail.com";
        private int smtpPort = 587;
        private string emailFromAddres = "recuperasrch.code@gmail.com";
        private string password = "alsdksjawbwjvdzi";

        public void AddEmailsToMailMessage(MailMessage mailMessage, string[] emails)
        {
            foreach (var email in emails)
            {
                mailMessage.To.Add(email);
            }
        }
        public void SendEmail(string[] email, string subject, string body, bool isHtml = false)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(emailFromAddres);
                AddEmailsToMailMessage(mailMessage, email);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = isHtml;
                using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(emailFromAddres, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mailMessage);
                }

            }

        }

    }
}
