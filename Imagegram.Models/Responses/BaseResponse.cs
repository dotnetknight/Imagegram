using Imagegram.Models.Models;
using System.Collections.Generic;

namespace Imagegram.Models.Responses
{
    public class BaseResponse
    {
        public IEnumerable<LinkModel> Links { get; set; } = null;
    }
}
