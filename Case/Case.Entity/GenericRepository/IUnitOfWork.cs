using Case.Entity.Domains.Orders;
using Case.Entity.Domains.Payments; 
using Case.Entity.Domains.Users;
using Case.Entity.GenericRepository.Repositories;

namespace Case.Entity.GenericRepository;

public interface IUnitOfWork
{
    IRepository<Order> Order { get; } 
    IRepository<PaymentTransaction> PaymentTransaction { get; }
    IRepository<User> User { get; } 

}