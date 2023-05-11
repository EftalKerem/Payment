using System.Xml.Serialization;

namespace Case.Business.Common.Models.Requests; 
public class IdentityValidationRequest
{
    [XmlElement(ElementName = "TCKimlikNo")]
    public string TC { get; set; }
    [XmlElement(ElementName = "Ad")]
    public string FirstName { get; set; }
    [XmlElement(ElementName = "Soyad")]
    public string LastName { get; set; }
    [XmlElement(ElementName = "DogumYili")]
    public int BirthYear { get; set; }
}