using Newtonsoft.Json;

namespace FinaktivaPT.Models
{
    public class AuthUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class TokenManagement
    {
        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }

    }


    public interface ITokenService
    {
        bool IsAuthenticated(CUUsuarios request, out string token);
    }

}
