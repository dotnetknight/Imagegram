using Imagegram.Models.DTOs;
using System.Collections.Generic;

namespace Imagegram.Models.Responses.Query
{
    public class CommentsQueryResponse : BaseResponse
    {
        public IEnumerable<CommentsDto> Comments { get; set; }
    }
}
