using System;

namespace Imagegram.Models.DTOs
{
    public class CommentsDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public AccountDto Creator { get; set; }
    }
}
