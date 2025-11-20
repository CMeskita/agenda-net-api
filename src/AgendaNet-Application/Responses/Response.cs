using AgendaNet_Domain.Entities;

namespace AgendaNet_Application.Responses
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Detalhe { get; set; }
    }
    public class ResponseEstablishment : Response
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ThemeColor { get; set; }
        public string LogoUrl { get; set; }
        public int itemStore { get; set; }
    }

    public class ResponseEstablishmentDcoument
    {
        public string Description { get; set; }
        public string Value { get; set; }
      
    }
    public class ResponseEstablishmentUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Roler { get; set; }
        public string EstablishmentId { get; set; }
   

    }
    public class ResponseEstablishmentUserContact
    {
        public string Description { get; set; }
        public string Value { get; set; }
        public string IsActive { get; set; }
        public string UserId { get; set; }
     
    }
    public class ResponseGetallEstablishment
    {
        public List<ResponseEstablishment> Dados { get; set; }
      
    }
    public class ResponseToken:Response
    {
       
        public string Token { get; set; }
        public List<ResponseListEstablishmentLogin> Dados { get; set; }


    }
    public class ResponseGetallTenant
    {
        public List<ResponseTenant> Dados { get; set; }

    }
    public class ResponseTenant
    {
        public string Id { get;  set; }
        public string EstablishmentId { get;  set; }

    }


    public class ResponseListEstablishmentLogin
    {
        public string EstablishmentId { get; set; }

    }
}
