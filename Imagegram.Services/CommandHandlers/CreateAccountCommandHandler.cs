using Imagegram.Domain.AccountsEntity;
using Imagegram.Infrastructure.Command;
using Imagegram.Models.Commands;
using Imagegram.Models.Responses.Command;
using Imagegram.Repository.Repository;
using System;
using System.Threading.Tasks;

namespace Imagegram.Services.CommandHandlers
{
    public class CreateAccountCommandHandler : ICommandHandlerAsync<CreateAccountCommand, CreateAccountCommandResponse>
    {
        #region Properties

        private readonly HateoasLinksService HateoasLinksService;
        private readonly MediaTypeCheckService mediaTypeCheckService;
        private readonly IRepository<Accounts> accountsRepository;

        #endregion

        #region Constructor
        public CreateAccountCommandHandler(
            HateoasLinksService HateoasLinksService,
            MediaTypeCheckService mediaTypeCheckService,
            IRepository<Accounts> accountsRepository)
        {
            this.HateoasLinksService = HateoasLinksService;
            this.mediaTypeCheckService = mediaTypeCheckService;
            this.accountsRepository = accountsRepository;
        }

        #endregion

        #region Handler
        public Task<CreateAccountCommandResponse> HandleAsync(CreateAccountCommand command)
        {
            try
            {
                accountsRepository.BeginTransaction();

                Guid accountId = Guid.NewGuid();

                accountsRepository.Insert(new Accounts()
                {
                    Id = accountId,
                    Name = command.Name
                });

                return Task.FromResult(new CreateAccountCommandResponse()
                {
                    Id = accountId,
                    Links = mediaTypeCheckService.ShouldSendHateoasLinks() ? HateoasLinksService.CreateLinksForImagegram(accountId) : null
                });
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
