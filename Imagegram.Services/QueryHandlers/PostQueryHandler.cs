using AutoMapper;
using Imagegram.Domain.PostsEntity;
using Imagegram.Infrastructure.Bus.Query;
using Imagegram.Models.DTOs;
using Imagegram.Models.Exceptions;
using Imagegram.Models.Queries;
using Imagegram.Models.Responses.Query;
using Imagegram.Repository.Repository;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Imagegram.Services.QueryHandlers
{
    public class PostQueryHandler : IQueryHandlerAsync<PostQuery, PostQueryResponse>
    {
        #region Properties

        private readonly IRepository<Posts> postsRepository;
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly HateoasLinksService HateoasLinksService;
        private readonly MediaTypeCheckService mediaTypeCheckService;
        private readonly IMapper mapper;

        #endregion

        #region Constructor

        public PostQueryHandler(
            IRepository<Posts> postsRepository,
            HateoasLinksService HateoasLinksService,
            MediaTypeCheckService mediaTypeCheckService,
            IActionContextAccessor actionContextAccessor,
            IMapper mapper)
        {
            this.postsRepository = postsRepository;
            this.HateoasLinksService = HateoasLinksService;
            this.mediaTypeCheckService = mediaTypeCheckService;
            this.actionContextAccessor = actionContextAccessor;
            this.mapper = mapper;
        }

        #endregion

        #region Handler
        public Task<PostQueryResponse> HandleAsync(PostQuery query)
        {
            try
            {
                var post = postsRepository.Get(new Guid(actionContextAccessor.ActionContext.RouteData.Values["PostId"].ToString()));

                if (post is null)
                    throw new PostNotFound("Post with requested id not found");

                var mappedPost = mapper.Map<PostDto>(post);
                mappedPost.Comments = mappedPost.Comments.OrderByDescending(comment => comment.CreatedAt);

                return Task.FromResult(new PostQueryResponse()
                {
                    Post = mappedPost,
                    Links = mediaTypeCheckService.ShouldSendHateoasLinks() ? HateoasLinksService.CreateLinksForImagegram(Guid.Empty) : null
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
