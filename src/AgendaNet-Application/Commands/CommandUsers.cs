using AgendaNet_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaNet_Application.Commands
{
    public class CommandUsers
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class CommandResetLogrinUsers
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }


    }
}
