using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaNet_email.Domain.Interfaces
{
    public interface IMailService
    {
       void SendEmail(string[] email, string subject, string body, bool isHtml = false);
    }
}
