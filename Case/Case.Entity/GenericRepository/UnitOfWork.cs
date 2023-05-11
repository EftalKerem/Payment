using Case.Entity.Domains.Orders;
using Case.Entity.Domains.Payments; 
using Case.Entity.Domains.Users;
using Case.Entity.Entities;
using Case.Entity.GenericRepository.Repositories;

namespace Case.Entity.GenericRepository;

public class UnitOfWork : IUnitOfWork
{
    private readonly CaseDbContext _caseDbContext;

    private IRepository<Order> _order; 
    private IRepository<User> _user;
    private IRepository<PaymentTransaction> _paymentTransaction;  

    public UnitOfWork(CaseDbContext caseDbContext) => _caseDbContext = caseDbContext;
         
    public IRepository<Order> Order => _order ??= new Repository<Order>(_caseDbContext);
  
    public IRepository<User> User => _user ??= new Repository<User>(_caseDbContext);

    public IRepository<PaymentTransaction> PaymentTransaction => _paymentTransaction ??= new Repository<PaymentTransaction>(_caseDbContext); 
 

}