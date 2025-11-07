namespace AgendaNet_Domain.Entities
{ 
    //uuários
    public class User
    {
        public string Id { get;protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string PasswordHash { get; protected set; }
        public string Roler { get; protected set; }
        public string register { get; protected set; }

        public string EstablishmentId { get; set; }
        public Establishment Establishment { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
