using AutoMapper;
using Imagegram.Domain.AccountsEntity;
using Imagegram.Infrastructure.Bus.Query;
using Imagegram.Models.DTOs;
using Imagegram.Models.Exceptions;
using Imagegram.Models.Queries;
using Imagegram.Models.Responses.Query;
using Imagegram.Repository.Repository;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Imagegram.Services.QueryHandlers
{
    public class AccountQueryHandler : IQueryHandlerAsync<AccountQuery, AccountQueryResponse>
    {
        #region Properties

        private readonly IRepository<Accounts> accountsRepository;
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly HateoasLinksService HateoasLinksService;
        private readonly MediaTypeCheckService mediaTypeCheckService;
        private readonly IMapper mapper;

        #endregion

        #region Constructor

        public AccountQueryHandler(
            IRepository<Accounts> accountsRepository,
            HateoasLinksService HateoasLinksService,
            MediaTypeCheckService mediaTypeCheckService,
            IActionContextAccessor actionContextAccessor,
            IMapper mapper)
        {
            this.accountsRepository = accountsRepository;
            this.HateoasLinksService = HateoasLinksService;
            this.mediaTypeCheckService = mediaTypeCheckService;
            this.actionContextAccessor = actionContextAccessor;
            this.mapper = mapper;
        }

        #endregion

        #region Handler

        public Task<AccountQueryResponse> HandleAsync(AccountQuery query)
        {
            try
            {
                var account = accountsRepository.Get(new Guid(actionContextAccessor.ActionContext.RouteData.Values["AccountId"].ToString()));

                if (account is null)
                    throw new AccountNotFound("Account with requested id not found");

                var mappedAccount = mapper.Map<AccountDto>(account);

                return Task.FromResult(new AccountQueryResponse()
                {
                    Account = mappedAccount,
                    Links = mediaTypeCheckService.ShouldSendHateoasLinks() ? HateoasLinksService.CreateLinksForImagegram(Guid.Empty) : null
                });
            }

            catch (AccountNotFound)
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
