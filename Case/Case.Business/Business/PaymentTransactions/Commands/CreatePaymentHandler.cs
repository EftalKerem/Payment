using Case.Business.Business.PaymentTransactions.Requests;
using Case.Business.Integrations.UnitedPayment.Models.Requests;
using Case.Business.Integrations.UnitedPayment.Models.Responses;
using Case.Business.Integrations.UnitedPayment.Services;
using Case.Entity.Domains.Enums;
using Case.Entity.Domains.Payments;
using Case.Entity.GenericRepository;
using MediatR;

namespace Case.Business.Business.PaymentTransactions.Commands;

public class CreatePaymentCommand : CreatePaymentRequest, IRequest<PaymentResponse>
{
}

public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, PaymentResponse>
{
    private readonly IUnitOfWork _uow;
    private readonly IUnitedPaymentService _ups;


    public CreatePaymentHandler(IUnitOfWork uow, IUnitedPaymentService ups)
    {
        _uow = uow;
        _ups = ups;
    }

    public async Task<PaymentResponse> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var order = await _uow.Order.FirstOrDefaultAsync(p => p.Id == request.OrderId);
        if (order == null) return null;
        var paymentRequest = new PaymentRequest();

        paymentRequest.CustomerId = order.UserId.ToString();

        #region CartInfo

        paymentRequest.CardNumber = request.CartInfo.CardNumber;
        paymentRequest.CardHolderName = request.CartInfo.CardHolderName;
        paymentRequest.ExpiryDateMonth = request.CartInfo.ExpiryDateMonth;
        paymentRequest.ExpiryDateYear = request.CartInfo.ExpiryDateYear;
        paymentRequest.Cvv = request.CartInfo.Cvv;

        #endregion

        #region OrderDetail

        paymentRequest.TxnType = request.TxnType.ToString();
        paymentRequest.InstallmentCount = "1";
        paymentRequest.Currency = "999";
        paymentRequest.OrderId = order.Id.ToString();
        paymentRequest.TotalAmount = CalculateOrderTotalAmount(order.TotalAmount);
        paymentRequest.Rnd = new Random().Next(1, 1000).ToString();

        #endregion

        paymentRequest.MemberId = 1;
        paymentRequest.MerchantId = 1894;
        paymentRequest.Hash = GetHashCode(paymentRequest);
        
        var response = await _ups.Pay(paymentRequest);

        if (!response.Fail && response.PaymentResult.ResponseCode == "00")
        {
            await _uow.PaymentTransaction.AddAsync(new PaymentTransaction()
            {
                Amount = CalculateTotalAmount(response.PaymentResult.TotalAmount),
                OrderId = order.Id,
                CardPan = request.CartInfo.CardNumber,
                UserId = order.UserId,
                Status = GetPaymentStatus(response.PaymentResult.TxnStatus),
                ResponseCode = response.PaymentResult.ResponseCode,
                ResponseMessage = response.PaymentResult.ResponseMessage,
                Type = (Enums.PaymentTransactionType)response.PaymentResult.TxnType,

            });
            await _uow.PaymentTransaction.SaveChangesAsync();
        }
        
        return response;
    }

    private string GetHashCode(PaymentRequest request)
    {
        var apiKey =
            "SKI0NDHEUP60J7QVCFATP9TJFT2OQFSO"; // Bu bilgi size özel olup kayıtlı kullanıcınıza mail olarak gönderilmiştir.

        var hashString =
            $"{apiKey}{request.UserCode}{request.Rnd}{request.TxnType}" +
            $"{request.TotalAmount}{request.CustomerId}{request.OrderId}";

        System.Security.Cryptography.SHA512 s512 = System.Security.Cryptography.SHA512.Create();

        System.Text.UnicodeEncoding ByteConverter = new System.Text.UnicodeEncoding();

        byte[] bytes = s512.ComputeHash(ByteConverter.GetBytes(hashString));

        var hash = System.BitConverter.ToString(bytes).Replace("-", "");

        return hash;
    }

    private Enums.PaymentTransactionStatus GetPaymentStatus(string txnStatus)
    {
        switch (txnStatus.ToUpper())
        {
            case "Y":
                return Enums.PaymentTransactionStatus.Success;
            case "E":
                return Enums.PaymentTransactionStatus.Error;
            case "P":
                return Enums.PaymentTransactionStatus.Waiting;
            case "V":
                return Enums.PaymentTransactionStatus.Canceled; 
            default:
                return Enums.PaymentTransactionStatus.Error;
        }
    }

    private decimal CalculateTotalAmount(string totalAmount)
    {
        var d = decimal.Parse(totalAmount);
        var s = d.ToString("00 00").Replace(" ",".");
        return decimal.Parse(s);
    }

    private string CalculateOrderTotalAmount(decimal totalAmount)
    {
        var text = totalAmount.ToString();
        if (text.Contains(','))
        {
            var splits = text.Split(',')
                .Last().Length;
            text += splits < 2 ? "0" : "";
            return text.Replace(",", "");
        } 
        return text += "00";
    }
}