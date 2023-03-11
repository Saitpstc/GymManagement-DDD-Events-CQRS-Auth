namespace Shared.Application.Contracts;

public abstract class CommandBase:ICommand
{

    protected CommandBase()
    {
        Id = Guid.NewGuid();
    }

    protected CommandBase(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

public abstract class CommandBase<TResult>:ICommand<TResult>
{
    protected CommandBase()
    {
        Id = Guid.NewGuid();
    }

    protected CommandBase(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}