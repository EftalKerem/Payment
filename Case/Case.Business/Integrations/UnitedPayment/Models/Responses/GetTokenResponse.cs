using System.Text.Json.Serialization;

namespace Case.Business.Integrations.UnitedPayment.Models.Responses;

public class GetTokenResponse
{
    [JsonPropertyName("fail")]
    public bool Fail{ get; set; }

    [JsonPropertyName("statusCode")]
    public int StatusCode{ get; set; }

    [JsonPropertyName("result")]
    public TokenResult TokenResult{ get; set; }

    [JsonPropertyName("count")]
    public int Count{ get; set; }

    [JsonPropertyName("responseCode")]
    public string ResponseCode{ get; set; }

    [JsonPropertyName("responseMessage")]
    public string ResponseMessage{ get; set; }
}
public class TokenResult
{
    [JsonPropertyName("userId")]
    public int UserId{ get; set; }

    [JsonPropertyName("token")]
    public string Token{ get; set; }
}