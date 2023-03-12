namespace Shared.Application.Contracts;

using Config.Commands;
using Config.Queries;

public abstract class CommandHandlerBase<TCommand, TResult>:ICommandHandler<TCommand, TResult> where TCommand: ICommand<TResult>
{
    readonly protected IErrorMessageCollector ErrorMessageCollector;

    protected CommandHandlerBase(IErrorMessageCollector errorMessageCollector)
    {
        ErrorMessageCollector = errorMessageCollector;
    }

    public abstract Task<TResult> Handle(TCommand request, CancellationToken cancellationToken);
}

public abstract class QueryHandlerBase<TQuery, TResult>:IQueryHandler<TQuery, TResult> where TQuery: IQuery<TResult>
{
    readonly protected IErrorMessageCollector ErrorMessageCollector;

    protected QueryHandlerBase(IErrorMessageCollector errorMessageCollector)
    {
        ErrorMessageCollector = errorMessageCollector;
    }

    public abstract Task<TResult> Handle(TQuery request, CancellationToken cancellationToken);
}