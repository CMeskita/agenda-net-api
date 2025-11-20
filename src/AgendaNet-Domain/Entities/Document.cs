namespace AgendaNet_Domain.Entities
{
    //Contatos
    public class Document
    {
        public Document()
        {

        }
        public Document(string description, string value, string establishid, string? usuerId)
        {
            Id = Guid.NewGuid().ToString().ToUpper();
            Description = description;
            Value = value;
            IsActive = true;
            EstablishmentId = establishid;
            UsuerId = usuerId;
        }

        public string Id { get; protected set; }
        public string Description { get; protected set; }
        public string Value { get; protected set; }
        public bool IsActive { get; protected set; }
        public string EstablishmentId { get; set; }
        public string? UsuerId { get; set; }
        public Establishment Establishment { get; set; }
    }
}
