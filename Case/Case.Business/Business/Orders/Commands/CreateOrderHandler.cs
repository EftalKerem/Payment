using Case.Business.Business.Orders.Responses;
using Case.Entity.Domains.Orders; 
using Case.Entity.GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Case.Business.Business.Orders.Commands;

public class CreateOrderCommand : IRequest<CreateOrderResponse>
{
    public long UserId { get; set; }
}
public class CreateOrderHandler : IRequestHandler<CreateOrderCommand,CreateOrderResponse>
{
    private readonly IUnitOfWork _uow;

    public CreateOrderHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderMax = _uow.Order.TableNoTracking().Count();
         
        var order = new Order()
        {
            OrderNumber = $"EKD{orderMax+1}",  
            UserId = request.UserId,
            TotalAmount = (decimal)1.00
        };
        
        await _uow.Order.AddAsync(order);
        await _uow.Order.SaveChangesAsync();
        
        return new CreateOrderResponse()
        {
            OrderId = order.Id
        };
    }
}