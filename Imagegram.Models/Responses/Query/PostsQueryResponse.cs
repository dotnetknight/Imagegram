using Imagegram.Models.DTOs;
using System.Collections.Generic;

namespace Imagegram.Models.Responses.Query
{
    public class PostsQueryResponse : BaseResponse
    {
        public IEnumerable<PostDto> Posts { get; set; }
    }
}
