using Imagegram.Domain.CommentsEntity;
using Imagegram.Domain.PostsEntity;
using System.Collections.Generic;

namespace Imagegram.Domain.AccountsEntity
{
    public class Accounts : BaseEntity
    {
        public string Name { get; set; }
        public virtual IList<Posts> Posts { get; set; }
        public virtual IList<Comments> Comments { get; set; }
    }
}
