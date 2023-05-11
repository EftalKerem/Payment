using Case.Business.Business.Users.Commands;
using Case.Business.Business.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Case.Api.Controllers;

public class UserController : BaseController
{  
    [HttpGet]
    public async Task<IActionResult> ValidTC(long userId)
    { 
        var request = new IdentityNoValidQuery
        {
            UserId = userId
        };
       var result = await Mediator.Send(request); 
       return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserQuery request)
    {  
        var result = await Mediator.Send(request); 
        return Ok(result);
    }
}