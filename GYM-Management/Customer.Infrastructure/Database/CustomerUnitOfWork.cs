namespace Customer.Infrastructure.Database;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;

public class CustomerUnitOfWork:UnitOfWork
{
    public CustomerUnitOfWork(IMediator mediator, ICustomerDbContext context):base(mediator, (DbContext) context)
    {
    }
}