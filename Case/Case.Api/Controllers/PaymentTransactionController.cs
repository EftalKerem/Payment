using Case.Business.Business.PaymentTransactions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Case.Api.Controllers;

public class PaymentTransactionController : BaseController
{ 
    [HttpPost]
    public async Task<IActionResult> CreatePayment(CreatePaymentCommand request)
    {  
        var result = await Mediator.Send(request); 
        return Ok(result);
    }
}