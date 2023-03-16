namespace Shared.Application.Contracts;

using Config.Queries;

public abstract class QueryHandlerBase<TQuery, TResult>:IQueryHandler<TQuery, TResult> where TQuery: IQuery<TResult>
{
    readonly protected IErrorMessageCollector ErrorMessageCollector;

    protected QueryHandlerBase(IErrorMessageCollector errorMessageCollector)
    {
        ErrorMessageCollector = errorMessageCollector;
    }

    public abstract Task<TResult> Handle(TQuery request, CancellationToken cancellationToken);
}