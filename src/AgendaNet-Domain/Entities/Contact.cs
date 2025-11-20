namespace AgendaNet_Domain.Entities
{
    //Contatos
    public class Contact
    {
        public Contact()
        {
            
        }
        public Contact(string description, string value,string userid)
        {
            Id = Guid.NewGuid().ToString().ToUpper();
            Description = description;
            Value = value;
            UserId = userid;
            IsActive = true;
        }

        public string Id { get;protected set; }
        public string Description { get;protected set; }
        public string Value { get;protected set; }
        public bool IsActive { get; protected set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
