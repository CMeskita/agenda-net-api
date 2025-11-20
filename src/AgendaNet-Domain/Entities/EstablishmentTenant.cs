

using System.Reflection.Metadata;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AgendaNet_Domain.Entities
{
    public class EstablishmentTenant
    {
        public EstablishmentTenant()
        {
            
        }
        public EstablishmentTenant(string id,string establishmentId, string email, Establishment establishment)
        {
            Id = id;
            EstablishmentId = establishmentId;
            Email = email;
            Register = DateTime.UtcNow.ToString();
            IsActive = true;
            Establisment = establishment;
        }
        public EstablishmentTenant( string establishmentId, string email,Establishment establishment)
        {
            Id = Guid.NewGuid().ToString().ToUpper();
            EstablishmentId = establishmentId;
            Email = email;
            Register = DateTime.UtcNow.ToString();
            IsActive = true;
            Establisment = establishment;
        }
        public void SetItemStore(int itemstore)
        {
            ItemStore=itemstore;
        }

        public void SetAtributosEstablismentJson(Establishment document)
        {
            AtributosEstablisment = JsonSerializer.Serialize(document); 

        }
   
        public string Id { get; protected set; }
        public string EstablishmentId { get; protected set; }
        public string Email { get; protected set; }
        public string Register { get; protected set; }
        public int ItemStore { get; protected set; }
        public bool IsActive { get; protected set; }
        public string AtributosEstablisment { get; set; }
        public Establishment Establisment { get; set; }
    }
}
