namespace Shared.Application.Contracts;

using MediatR;

public interface ICommand<out TResult>:IRequest<TResult>
{
}

public interface ICommand:IRequest<Unit>
{
    Guid Id { get; }
}