using System.Xml.Serialization;
using Case.Business.Common.Models.Requests;
using Case.Business.KPSPublic;
using RestSharp;

namespace Case.Business.Common;

public static class IdentityValidation
{
    public static async Task<bool> Validation(IdentityValidationRequest model)
    { 
        var client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
        var response =
            await client.TCKimlikNoDogrulaAsync(
                long.Parse(model.TC),
                model.FirstName,
                model.LastName,
                model.BirthYear
            );
        return response.Body.TCKimlikNoDogrulaResult;
    } 
}