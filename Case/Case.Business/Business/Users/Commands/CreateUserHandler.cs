using Case.Business.Business.Users.Requests;
using Case.Business.Business.Users.Responses;
using Case.Entity.Domains.Users;
using Case.Entity.GenericRepository;
using MediatR;

namespace Case.Business.Business.Users.Commands;

public class CreateUserQuery : CreateUserRequest , IRequest<CreateUserResponse>{

    
}
public class CreateUserHandler : IRequestHandler<CreateUserQuery, CreateUserResponse>
{
    private readonly IUnitOfWork _uow;
    public CreateUserHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<CreateUserResponse> Handle(CreateUserQuery request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Name = request.Name,
            Surname = request.Surname,
            IdentityNo = request.IdentityNo,
            BirthDate = request.BirthDate
        };
        
        await _uow.User.AddAsync(user);
        await _uow.User.SaveChangesAsync(); 
        
        return new CreateUserResponse(){UserId = user.Id};
        
    }
}