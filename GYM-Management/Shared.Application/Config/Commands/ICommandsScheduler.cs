namespace Shared.Application.Config.Commands;

using Contracts;

public interface ICommandsScheduler
{
    Task EnqueueAsync<T>(ICommand<T> command);
}