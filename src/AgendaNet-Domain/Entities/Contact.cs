namespace AgendaNet_Domain.Entities
{
    //Contatos
    public class Contact
    {
        public string Id { get;protected set; }
        public string Description { get;protected set; }
        public string Value { get;protected set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
