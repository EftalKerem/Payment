namespace Case.Entity.Domains.Users;

public class User : BaseEntity
{
    public string Name { get; set; }  
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public string IdentityNo { get; set; }
    public bool IdentityNoVerified { get; set; }
    public Enums.Enums.UserStatus Status { get; set; }
}