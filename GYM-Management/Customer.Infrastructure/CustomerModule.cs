namespace Customer.Infrastructure;

using Application.Contracts;
using MediatR;
using Shared.Application.Contracts;

class CustomerModule:ICustomerModule
{
    private readonly IMediator _mediator;

    public CustomerModule(IMediator mediator)
    {
        _mediator = mediator;

    }

    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
    {
        return await _mediator.Send(command);
    }

    public async Task<Unit> ExecuteCommandAsync(ICommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        return await _mediator.Send(query);
    }
}