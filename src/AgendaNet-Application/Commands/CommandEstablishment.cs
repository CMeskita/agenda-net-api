using AgendaNet_Domain.Entities;

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
     
        //public ICollection<CommanEstablishmentDcoument> Documents { get; set; }
        //public ICollection<CommandEstablishmentUser> Users { get; set; }

        public static implicit operator Establishment(CommandEstablishment dto) 
            => new Establishment(dto.Name, dto.Description, dto.Address, dto.PhoneNumber, dto.Email,dto.ThemeColor,dto.LogoUrl);
    }
    public class CommanEstablishmentDcoument
    {
        public string Description { get;  set; }
        public string Value { get;  set; }
        public string EstablishmentId { get; set; }
        public string UsuerId { get; set; }
        public static implicit operator Document(CommanEstablishmentDcoument dto)
            => new Document(dto.Description, dto.Value,dto.EstablishmentId,dto.UsuerId);
    }
    public class CommandEstablishmentUser
    {
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string PasswordHash { get;  set; }
        public string Roler { get;  set; }
        public string EstablishmentId { get; set; }
 

        public static implicit operator User(CommandEstablishmentUser dto)
            => new User(dto.Name, dto.Email, dto.PasswordHash,dto.EstablishmentId);

    }
    public class CommandEstablishmentUserContact
    {
        public string Description { get; set; }
        public string Value { get; set; }
        public string IsActive { get; set; }
        public string UserId { get; set; }
        public static implicit operator Contact(CommandEstablishmentUserContact dto)
            => new Contact(dto.Description,dto.Value,dto.UserId);
    }
    public class CommandGetAllEstablishment
    {
        public string EstablishmentTenantId { get; set; }
    }
    public class CommandGetIdEstablishment
    {
        public string Id { get; set; }
    }
    public class CommandDuplicateEstablishment
    {
        public string TenantId { get; set; }

    }
    public class CommandGetAllTenants
    {
    }
}
