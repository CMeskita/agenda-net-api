using AgendaNet_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AgendaNet_Application.Commands
{
    public class CommandEstablishment
    {
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string Address { get;  set; }
        public string PhoneNumber { get;  set; }
        public string Email { get;  set; }
        public string ThemeColor { get;  set; }
        public string LogoUrl { get;  set; }
        public static implicit operator Establishment(CommandEstablishment dto) 
            => new Establishment(dto.Name, dto.Description, dto.Address, dto.PhoneNumber, dto.Email,dto.ThemeColor,dto.LogoUrl);
    }
    public class CommandGetAllEstablishment
    {
        //public string EstablishmentId { get; set; }
    }
}
