using System.ComponentModel.DataAnnotations.Schema;
using Case.Entity.Domains.Users;

namespace Case.Entity.Domains.Orders;

public class Order : BaseEntity
{
    public string OrderNumber { get; set; }

    [ForeignKey("User")]
    public long UserId { get; set; }
    public User User { get; set; }   
    public decimal TotalAmount { get; set; }
}