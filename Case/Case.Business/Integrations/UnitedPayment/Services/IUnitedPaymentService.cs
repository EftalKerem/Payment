using Case.Business.Integrations.UnitedPayment.Models.Requests;
using Case.Business.Integrations.UnitedPayment.Models.Responses;

namespace Case.Business.Integrations.UnitedPayment.Services;

public interface IUnitedPaymentService
{
    Task<PaymentResponse> Pay(PaymentRequest paymentRequest);
}