namespace Shared.Application.Config.Commands
{
    using System;
    using Shared.Application.Contracts;

    public abstract class InternalCommandBase : ICommand
    {
        protected InternalCommandBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }

    public abstract class InternalCommandBase<TResult> : ICommand<TResult>
    {
        protected InternalCommandBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected InternalCommandBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}