
namespace AgendaNet.Auth.Domain.Models
{
    public class Tokens
    {

        public string Access_Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevorked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public Tokens(string accessToken, string jwtId, DateTime expiryDate)
        {
            Access_Token = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            JwtId = jwtId ?? throw new ArgumentNullException(nameof(jwtId));
            ExpiryDate = expiryDate;
            AddedDate = DateTime.UtcNow;
            IsUsed = false;
            IsRevorked = false;
        }
    }
}
