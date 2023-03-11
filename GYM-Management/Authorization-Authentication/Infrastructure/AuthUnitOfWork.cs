namespace Authorization_Authentication.Infrastructure;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

public class AuthUnitOfWork:UnitOfWork
{
    public AuthUnitOfWork(IMediator mediator, IAuthDbContext context):base(mediator, (DbContext) context)
    {

    }
}