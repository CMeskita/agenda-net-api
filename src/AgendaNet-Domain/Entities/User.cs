namespace AgendaNet_Domain.Entities
{ 
    //uuários
    public class User
    {
        public User()
        {
                
        }
        public User(string id,string name, string email, string passwordHash,string roler, string establishid)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Roler = roler;
            EstablishmentId = establishid;
        }
        public User(string name, string email, string passwordHash, string establishid)
        {
            Id= Guid.NewGuid().ToString().ToUpper();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Roler = UserRoler.Admin.ToString(); 
            Register = DateTime.UtcNow.ToString();
            EstablishmentId = establishid;
            IsActive = true;
        }
        public void setAcessed(bool isacessed)
        {
            IsAcessed = isacessed;
        }
        public string Id { get;protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string PasswordHash { get; protected set; }
        public string Roler { get; protected set; }
        public string Register { get; protected set; }
        public bool IsActive { get; protected set; }
        public bool IsAcessed { get; protected set; }
        public string EstablishmentId { get; set; }
        public Establishment Establishment { get; set; }

        public ICollection<Contact> Contacts { get; set; }


        public enum UserRoler
        {
            Admin,
            User
        }
    }
}
