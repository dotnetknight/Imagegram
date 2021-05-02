using Imagegram.Infrastructure.Query;
using Imagegram.Models.ResourceParameters;

namespace Imagegram.Models.Queries
{
    public class PostsQuery : IQuery
    {
        public PostsResourceParameters postsResourceParameters { get; set; }

        public PostsQuery(PostsResourceParameters postsResourceParameters)
        {
            this.postsResourceParameters = postsResourceParameters;
        }
    }
}
