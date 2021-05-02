using Imagegram.Infrastructure.Command;
using System.Threading.Tasks;

namespace Imagegram.Infrastructure.Bus.Command
{
    public interface ICommandBusAsync
    {
        Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> ExecuteAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand;
    }
}
