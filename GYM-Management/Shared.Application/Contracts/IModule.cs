namespace Shared.Application.Contracts
{
    using System.Threading.Tasks;
    using MediatR;

    public interface IModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task<Unit> ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}