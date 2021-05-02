using System;
using System.Collections.Generic;

namespace Imagegram.Models.DTOs
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<CommentsDto> Comments { get; set; }
    }
}
