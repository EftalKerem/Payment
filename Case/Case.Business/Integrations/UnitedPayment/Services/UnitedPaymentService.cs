using System.Text.Json.Serialization;
using Case.Business.Integrations.UnitedPayment.Models.Requests;
using Case.Business.Integrations.UnitedPayment.Models.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace Case.Business.Integrations.UnitedPayment.Services;

public class UnitedPaymentService : IUnitedPaymentService
{
    public async Task<PaymentResponse> Pay(PaymentRequest paymentRequest)
    {
        var token = await GetToken();
        
        var client = new RestClient();
        var request = new RestRequest("https://ppgpayment-test.birlesikodeme.com:20000/api/ppg/Payment/NoneSecurePayment", Method.Post);
        request.AddHeader("Content-Type", "application/json");  
        request.AddHeader("Authorization", $"Bearer {token}");  
        request.AddJsonBody(JsonConvert.SerializeObject(paymentRequest)); 
        var response = await client.ExecuteAsync<PaymentResponse>(request);
        
        if (response.Content != null && response.Data != null && !response.Data.Fail)
        {
            return response.Data;
        } 
        return new PaymentResponse(){Fail = true};
    }

    private static async Task<string> GetToken()
    {
        var client = new RestClient();
        var request = new RestRequest("https://ppgsecurity-test.birlesikodeme.com:55002/api/ppg/Securities/authenticationMerchant", Method.Post);
        request.AddHeader("Content-Type", "application/json"); 
        var body = JsonConvert.SerializeObject(new GetTokenRequest());
        request.AddStringBody(body, DataFormat.Json);
        var response = await client.ExecuteAsync<GetTokenResponse>(request);
        
        if (response.Content != null && response.Data != null && !response.Data.Fail)
        {
            return response.Data.TokenResult.Token;
        }

        return string.Empty;
    }
}