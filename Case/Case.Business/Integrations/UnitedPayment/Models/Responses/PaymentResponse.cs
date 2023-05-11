using System.Text.Json.Serialization;

namespace Case.Business.Integrations.UnitedPayment.Models.Responses;

public class PaymentResponse
{
    [JsonPropertyName("fail")]
    public bool Fail{ get; set; }

    [JsonPropertyName("statusCode")]
    public int StatusCode{ get; set; }

    [JsonPropertyName("result")]
    public PaymentResult PaymentResult{ get; set; }
}
public class PaymentResult
{
    [JsonPropertyName("responseCode")]
    public string ResponseCode{ get; set; }

    [JsonPropertyName("responseMessage")]
    public string ResponseMessage{ get; set; }

    [JsonPropertyName("orderId")]
    public string OrderId{ get; set; }

    [JsonPropertyName("txnType")]
    public object TxnType{ get; set; }

    [JsonPropertyName("txnStatus")]
    public string TxnStatus{ get; set; }

    [JsonPropertyName("vposId")]
    public int VposId{ get; set; }

    [JsonPropertyName("vposName")]
    public string VposName{ get; set; }

    [JsonPropertyName("authCode")]
    public string AuthCode{ get; set; }

    [JsonPropertyName("hostReference")]
    public object HostReference{ get; set; }

    [JsonPropertyName("totalAmount")]
    public string TotalAmount{ get; set; }
}