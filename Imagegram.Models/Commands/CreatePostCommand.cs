using Imagegram.Infrastructure.Command;
using Microsoft.AspNetCore.Http;

namespace Imagegram.Models.Commands
{
    public class CreatePostCommand : ICommand
    {
        public IFormFile Image { get; set; }
    }
}
