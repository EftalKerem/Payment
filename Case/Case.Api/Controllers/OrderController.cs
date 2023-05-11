using Case.Business.Business.Orders.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Case.Api.Controllers;

public class OrderController : BaseController
{ 
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand request)
    {  
        var result = await Mediator.Send(request); 
        return Ok(result);
    }
}