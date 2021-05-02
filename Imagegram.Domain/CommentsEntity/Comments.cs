using Imagegram.Domain.AccountsEntity;
using Imagegram.Domain.PostsEntity;
using System;

namespace Imagegram.Domain.CommentsEntity
{
    public class Comments : BaseEntity
    {
        public string Content { get; set; }
        public virtual Accounts Creator { get; set; }
        public virtual Posts Post { get; set; }
        public Guid PostId { get; set; }
        public Guid CreatorId { get; set; }
    }
}
