using Imagegram.Models.DTOs;

namespace Imagegram.Models.Responses.Query
{
    public class PostQueryResponse : BaseResponse
    {
        public PostDto Post { get; set; }
    }
}
