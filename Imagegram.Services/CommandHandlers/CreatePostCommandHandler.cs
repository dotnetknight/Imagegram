using Imagegram.Domain.AccountsEntity;
using Imagegram.Domain.PostsEntity;
using Imagegram.Infrastructure.Command;
using Imagegram.Models.Commands;
using Imagegram.Models.Exceptions;
using Imagegram.Models.Responses.Command;
using Imagegram.Repository.Repository;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Imagegram.Services.CommandHandlers
{
    public class CreatePostCommandHandler : ICommandHandlerAsync<CreatePostCommand, CreatePostCommandResponse>
    {
        #region Properties

        private readonly FileService fileService;
        private readonly HateoasLinksService HateoasLinksService;
        private readonly MediaTypeCheckService mediaTypeCheckService;
        private readonly IRepository<Posts> postsRepository;
        private readonly IRepository<Accounts> accountsRepository;
        private readonly IActionContextAccessor actionContextAccessor;

        #endregion

        #region Constructor
        public CreatePostCommandHandler(
            FileService fileService,
            HateoasLinksService HateoasLinksService,
            MediaTypeCheckService mediaTypeCheckService,
            IActionContextAccessor actionContextAccessor,
            IRepository<Accounts> accountsRepository,
            IRepository<Posts> postsRepository)
        {
            this.fileService = fileService;
            this.HateoasLinksService = HateoasLinksService;
            this.mediaTypeCheckService = mediaTypeCheckService;
            this.postsRepository = postsRepository;
            this.accountsRepository = accountsRepository;
            this.actionContextAccessor = actionContextAccessor;
        }

        #endregion

        #region Handler
        public Task<CreatePostCommandResponse> HandleAsync(CreatePostCommand command)
        {
            try
            {
                var creator = accountsRepository.Get(new Guid(actionContextAccessor.ActionContext.HttpContext.Request.Headers["X-Account-Id"]));

                if (creator is null)
                    throw new AccountNotFound("Account with requested id not found");

                postsRepository.BeginTransaction();

                var postId = Guid.NewGuid();

                var imageUrl = fileService.Upload(command.Image, postId);

                postsRepository.Insert(new Posts
                {
                    Id = postId,
                    Creator = creator,
                    ImageUrl = imageUrl
                });

                return Task.FromResult(new CreatePostCommandResponse()
                {
                    Id = postId,
                    Links = mediaTypeCheckService.ShouldSendHateoasLinks() ? HateoasLinksService.CreateLinksForImagegram(postId) : null
                });
            }

            catch (AccountNotFound)
            {
                throw;
            }

            catch (Exception)
            {
                postsRepository.RollbackTransaction();
                throw;
            }

            finally
            {
                postsRepository.CommitTransaction();
            }
        }

        #endregion
    }
}
