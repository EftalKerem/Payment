using System.ComponentModel.DataAnnotations.Schema;
using Case.Entity.Domains.Orders;
using Case.Entity.Domains.Users;

namespace Case.Entity.Domains.Payments;

public class PaymentTransaction : BaseEntity
{
    [ForeignKey("User")]
    public long UserId { get; set; }
    public User User { get; set; } 
    [ForeignKey("Order")]
    public long OrderId { get; set; }
    public Order Order { get; set; } 
    
    public Enums.Enums.PaymentTransactionType Type { get; set; } 
    public decimal Amount { get; set; } 
    public string CardPan { get; set; } 
    public string ResponseCode { get; set; }
    public string ResponseMessage { get; set; } 
    public Enums.Enums.PaymentTransactionStatus Status { get; set; }

}