using System.Text.Json.Serialization;

namespace LADomain
{
    public class Token
    {

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}
