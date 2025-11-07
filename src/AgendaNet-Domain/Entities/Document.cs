namespace AgendaNet_Domain.Entities
{
    //Contatos
    public class Document
    {
        public string Id { get;protected set; }
        public string Description { get;protected set; }
        public string Value { get;protected set; }
        public string IsActive { get; protected set; }
        public string EstablishmentId { get; set; }
        public Establishment Establishment { get; set; }
    }
}
