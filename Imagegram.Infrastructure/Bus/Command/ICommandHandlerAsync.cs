using System.Threading.Tasks;

namespace Imagegram.Infrastructure.Command
{
    public interface ICommandHandlerAsync<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }

    public interface ICommandHandlerAsync<TCommand, TResult> where TCommand : ICommand
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
