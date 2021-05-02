using Imagegram.Models.Models;
using System.Collections.Generic;

namespace Imagegram.Models.Responses
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
