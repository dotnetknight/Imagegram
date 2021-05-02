using Imagegram.Domain.AccountsEntity;
using Imagegram.Domain.CommentsEntity;
using System;
using System.Collections.Generic;

namespace Imagegram.Domain.PostsEntity
{
    public class Posts : BaseEntity
    {
        public string ImageUrl { get; set; }
        public virtual Accounts Creator { get; set; }
        public virtual IList<Comments> Comments { get; set; }
        public Guid CreatorId { get; set; }
    }
}
