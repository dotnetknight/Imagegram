using AutoMapper;
using Imagegram.Domain.PostsEntity;
using Imagegram.Infrastructure.Bus.Query;
using Imagegram.Models.DTOs;
using Imagegram.Models.Queries;
using Imagegram.Models.Responses.Query;
using Imagegram.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imagegram.Services.QueryHandlers
{
    public class PostsQueryHandler : IQueryHandlerAsync<PostsQuery, PostsQueryResponse>
    {
        #region Properties

        private readonly IRepository<Posts> postsRepository;
        private readonly HateoasLinksService HateoasLinksService;
        private readonly MediaTypeCheckService mediaTypeCheckService;
        private readonly IMapper mapper;

        #endregion

        #region Constructor

        public PostsQueryHandler(
            IRepository<Posts> postsRepository,
            HateoasLinksService HateoasLinksService,
            MediaTypeCheckService mediaTypeCheckService,
            IMapper mapper)
        {
            this.postsRepository = postsRepository;
            this.HateoasLinksService = HateoasLinksService;
            this.mediaTypeCheckService = mediaTypeCheckService;
            this.mapper = mapper;
        }

        #endregion

        #region Handler
        public Task<PostsQueryResponse> HandleAsync(PostsQuery query)
        {
            try
            {
                var posts = postsRepository.GetAll(query.postsResourceParameters);

                var mappedPosts = mapper.Map<IEnumerable<PostDto>>(posts);
                mappedPosts = mappedPosts.OrderByDescending(m => m.Comments.Count());

                foreach (var post in mappedPosts)
                {
                    post.Comments = post.Comments
                         .OrderByDescending(comment => comment.CreatedAt)
                         .Take(3);
                }

                return Task.FromResult(new PostsQueryResponse()
                {
                    Posts = mappedPosts,
                    Links = mediaTypeCheckService.ShouldSendHateoasLinks() ? HateoasLinksService.CreateLinksForImagegram(Guid.Empty) : null
                });
            }

            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
