namespace Shared.Application.Config.Queries
{
    using System;
    using Contracts;

    public abstract class QueryBase<TResult>:IQuery<TResult>
    {
        protected QueryBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected QueryBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}