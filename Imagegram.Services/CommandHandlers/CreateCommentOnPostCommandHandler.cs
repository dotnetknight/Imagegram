using Imagegram.Domain.AccountsEntity;
using Imagegram.Domain.CommentsEntity;
using Imagegram.Domain.PostsEntity;
using Imagegram.Infrastructure.Command;
using Imagegram.Models.Commands;
using Imagegram.Models.Exceptions;
using Imagegram.Repository.Repository;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Imagegram.Services.CommandHandlers
{
    public class CreateCommentOnPostCommandHandler : ICommandHandlerAsync<CreateCommentOnPostCommand>
    {
        #region Properties

        private readonly IRepository<Comments> commentsRepository;
        private readonly IRepository<Accounts> accountsRepository;
        private readonly IRepository<Posts> postsRepository;
        private readonly IActionContextAccessor actionContextAccessor;

        #endregion

        #region Constructor

        public CreateCommentOnPostCommandHandler(
            IRepository<Comments> commentsRepository,
            IRepository<Accounts> accountsRepository,
            IRepository<Posts> postsRepository,
            IActionContextAccessor actionContextAccessor)
        {
            this.accountsRepository = accountsRepository;
            this.postsRepository = postsRepository;
            this.commentsRepository = commentsRepository;
            this.actionContextAccessor = actionContextAccessor;
        }

        #endregion

        #region Handler
        public Task HandleAsync(CreateCommentOnPostCommand command)
        {
            try
            {
                var creator = accountsRepository.Get(new Guid(actionContextAccessor.ActionContext.HttpContext.Request.Headers["X-Account-Id"]));

                if (creator is null)
                    throw new AccountNotFound("Account with requested id not found");

                var post = postsRepository.Get(new Guid(actionContextAccessor.ActionContext.RouteData.Values["PostId"].ToString()));

                if (post is null)
                    throw new PostNotFound("Post with requested id not found");

                commentsRepository.BeginTransaction();

                commentsRepository.Insert(new Comments()
                {
                    Id = Guid.NewGuid(),
                    Creator = creator,
                    Content = command.Content,
                    CreatedAt = DateTime.Now,
                    PostId = post.Id
                });

                commentsRepository.CommitTransaction();

                return Task.CompletedTask;
            }

            catch (AccountNotFound)
            {
                throw;
            }

            catch (PostNotFound)
            {
                throw;
            }

            catch (Exception)
            {
                commentsRepository.RollbackTransaction();
                throw;
            }

            finally
            {
                commentsRepository.CommitTransaction();
            }
        }
        #endregion
    }
}
