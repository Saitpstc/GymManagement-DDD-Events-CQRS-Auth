namespace Shared.Application.Config.Queries
{
    using Contracts;
    using MediatR;

    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}