using System;

namespace Imagegram.Models.Responses.Command
{
    public class CreatePostCommandResponse : BaseResponse
    {
        public Guid Id { get; set; }
    }
}
