namespace Shared.Application.Contracts
{
    using System;
    using MediatR;

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }

    public interface ICommand : IRequest<Unit>
    {
        Guid Id { get; }
    }
}