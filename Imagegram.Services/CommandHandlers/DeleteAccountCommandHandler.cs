using Imagegram.Domain.AccountsEntity;
using Imagegram.Domain.CommentsEntity;
using Imagegram.Infrastructure.Command;
using Imagegram.Models.Commands;
using Imagegram.Models.Exceptions;
using Imagegram.Repository.Repository;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Imagegram.Services.CommandHandlers
{
    public class DeleteAccountCommandHandler : ICommandHandlerAsync<DeleteAccountCommand>
    {
        #region Properties

        private readonly IRepository<Accounts> accountsRepository;
        private readonly IRepository<Comments> commentsRepository;
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly FileService fileService;

        #endregion

        #region Constructor

        public DeleteAccountCommandHandler(
            IRepository<Accounts> accountsRepository,
            IRepository<Comments> commentsRepository,
            IActionContextAccessor actionContextAccessor,
            FileService fileService)
        {
            this.accountsRepository = accountsRepository;
            this.commentsRepository = commentsRepository;
            this.actionContextAccessor = actionContextAccessor;
            this.fileService = fileService;
        }

        #endregion

        #region Handler

        public Task HandleAsync(DeleteAccountCommand command)
        {
            try
            {
                var creator = accountsRepository.Get(new Guid(actionContextAccessor.ActionContext.RouteData.Values["AccountId"].ToString()));

                if (creator is null)
                    throw new AccountNotFound("Account with requested id not found");

                foreach (var post in creator.Posts)
                {
                    fileService.Delete(post.ImageUrl);
                    commentsRepository.DeleteAll(post.Comments);
                }

                accountsRepository.BeginTransaction();
                accountsRepository.Delete(creator);

                return Task.CompletedTask;
            }

            catch (AccountNotFound)
            {
                throw;
            }

            catch (Exception)
            {
                accountsRepository.RollbackTransaction();
                throw;
            }

            finally
            {
                accountsRepository.CommitTransaction();
            }
        }

        #endregion
    }
}
