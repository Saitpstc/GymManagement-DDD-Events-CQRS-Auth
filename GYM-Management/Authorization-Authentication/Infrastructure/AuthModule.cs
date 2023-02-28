namespace Authorization_Authentication.Infrastructure;

using Application.Contracts;
using MediatR;
using Shared.Application.Contracts;

public class AuthModule:IAuthModule
{
    private readonly IMediator _mediator;

    public AuthModule(IMediator mediator)
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