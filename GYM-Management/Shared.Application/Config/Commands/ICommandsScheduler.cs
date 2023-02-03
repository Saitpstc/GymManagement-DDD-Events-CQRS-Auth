namespace Shared.Application.Config.Commands
{
    using System.Threading.Tasks;
    using Shared.Application.Contracts;

    public interface ICommandsScheduler
    {
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}