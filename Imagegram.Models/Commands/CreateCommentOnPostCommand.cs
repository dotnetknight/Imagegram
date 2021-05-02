using Imagegram.Infrastructure.Command;

namespace Imagegram.Models.Commands
{
    public class CreateCommentOnPostCommand : ICommand
    {
        public string Content { get; set; }
    }
}
