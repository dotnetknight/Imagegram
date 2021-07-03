using AutoMapper;
using Imagegram.Domain.PostsEntity;
using Imagegram.Infrastructure.Bus.Query;
using Imagegram.Models.DTOs;
using Imagegram.Models.Exceptions;
using Imagegram.Models.Queries;
using Imagegram.Models.Responses.Query;
using Imagegram.Repository.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Imagegram.Services.QueryHandlers
{
    public class TestQueryHandler : IQueryHandlerAsync<PostQuery, PostQueryResponse>
    {
        #region Properties

        private readonly IRepository<Posts> postsRepository;
        private readonly IMapper mapper;

        #endregion

        #region Constructor

        public TestQueryHandler(
            IRepository<Posts> postsRepository,
            IMapper mapper)
        {
            this.postsRepository = postsRepository;
            this.mapper = mapper;
        }

        #endregion

        #region Handler
        public Task<PostQueryResponse> HandleAsync(PostQuery query)
        {
            try
            {
                var post = postsRepository.Get(query.PostId);

                if (post is null)
                    throw new PostNotFound("Post with requested id not found");

                var mappedPost = mapper.Map<PostDto>(post);
                mappedPost.Comments = mappedPost.Comments.OrderByDescending(comment => comment.CreatedAt);

                return Task.FromResult(new PostQueryResponse()
                {
                    Post = mappedPost
                });
            }

            catch (PostNotFound)
            {
                throw;
            }

            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
