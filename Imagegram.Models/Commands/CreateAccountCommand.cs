using Imagegram.Infrastructure.Command;

namespace Imagegram.Models.Commands
{
    public class CreateAccountCommand : ICommand
    {
        public string Name { get; set; }
    }
}
