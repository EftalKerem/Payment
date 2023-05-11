using System.Text.Json.Serialization;

namespace Case.Business.Integrations.UnitedPayment.Models.Requests;

public class GetTokenRequest
{  
    public GetTokenRequest()
    {
        ApiKey = "kU8@iP3@";
        Email = "murat.karayilan@dotto.com.tr";
        Lang = "TR";
    }

    [JsonPropertyName("ApiKey")] public string ApiKey { get; set; }

    [JsonPropertyName("Email")] public string Email { get; set; }

    [JsonPropertyName("Lang")] public string Lang { get; set; }
}