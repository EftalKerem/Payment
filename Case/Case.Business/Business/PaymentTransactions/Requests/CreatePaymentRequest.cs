namespace Case.Business.Business.PaymentTransactions.Requests;

public class CreatePaymentRequest
{
    public long OrderId { get; set; }
    public CartInfo CartInfo { get; set; }
    public TxnType TxnType { get; set; }
 
}

public class CartInfo
{
    public string CardNumber { get; set; }
    public string ExpiryDateMonth { get; set; }
    public string ExpiryDateYear { get; set; }
    public string Cvv { get; set; }
    public string CardHolderName { get; set; }
}

public enum TxnType
{
    Auth
}