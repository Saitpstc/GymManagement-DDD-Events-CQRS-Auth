namespace Shared.Application.Contracts;

using Config.Commands;

public abstract class CommandHandlerBase<TCommand, TResult>:ICommandHandler<TCommand, TResult> where TCommand: ICommand<TResult>
{
    readonly protected IErrorMessageCollector ErrorMessageCollector;

    protected CommandHandlerBase(IErrorMessageCollector errorMessageCollector)
    {
        ErrorMessageCollector = errorMessageCollector;
    }

    public abstract Task<TResult> Handle(TCommand request, CancellationToken cancellationToken);
}