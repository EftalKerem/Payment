using Case.Business.Common;
using Case.Business.Common.Models.Requests;
using Case.Business.KPSPublic;
using Case.Entity.GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Case.Business.Business.Users.Queries;

public class IdentityNoValidQuery : IRequest<bool>
{
    public long UserId { get; set; }
}

public class IdentityNoValidHandler : IRequestHandler<IdentityNoValidQuery,bool>
{
    private readonly IUnitOfWork _uow;

    public IdentityNoValidHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    public async Task<bool> Handle(IdentityNoValidQuery request, CancellationToken cancellationToken)
    {
        var user = await _uow.User.FirstOrDefaultAsync(p =>!p.IdentityNoVerified && p.Id == request.UserId );
        if (user != null)
        {
            var result = await IdentityValidation.Validation(new IdentityValidationRequest()
            {
                BirthYear = user.BirthDate.Date.Year,
                FirstName = user.Name,
                LastName = user.Surname,
                TC = user.IdentityNo
            });
            
            if (result)
            {
                user.IdentityNoVerified = true;
                await _uow.User.SaveChangesAsync();
            } 
            return result;
        }

        return false;
    }
}