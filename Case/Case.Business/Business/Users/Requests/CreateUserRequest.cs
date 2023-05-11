namespace Case.Business.Business.Users.Requests;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string IdentityNo { get; set; }
    public DateTime BirthDate { get; set; }
}