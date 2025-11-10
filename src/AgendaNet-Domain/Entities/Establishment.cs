namespace AgendaNet_Domain.Entities
{
    //Estabelecimentos
    public class Establishment
    {
        public Establishment(string name, string description, string address, string phoneNumber, string email, string themeColor, string logoUrl)
        {
            Id = Guid.NewGuid().ToString().ToUpper();
            Name = name;
            Description = description;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            ThemeColor = themeColor;
            LogoUrl = logoUrl;
            IsActive=true;
        }

        public string Id { get;protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Address { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public string Email { get; protected set; }
        public string ThemeColor { get; protected set; }
        public string LogoUrl { get; protected set; }
        public bool IsActive { get; protected set; }

        public ICollection<User> Users { get; set; }


    }
}
