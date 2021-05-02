using Imagegram.Infrastructure.Bus.Command;
using Imagegram.Infrastructure.Bus.Query;
using Imagegram.Models.Commands;
using Imagegram.Models.Models;
using Imagegram.Models.Queries;
using Imagegram.Models.Responses.Command;
using Imagegram.Models.Responses.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Imagegram.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ICommandBusAsync commandBus;
        private readonly IQueryBusAsync queryBus;

        public AccountsController(
            ICommandBusAsync commandBus,
            IQueryBusAsync queryBus)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;
        }

        /// <summary>
        /// Creates new account in the system
        /// </summary>
        /// <param name="command"></param>
        /// <response code="201">Creates new account in the system</response>
        /// <response code="400">Unable to add new user in the system due to validation error</response>
        /// <response code="500">Unable to add new user in the system due to validation error</response>
        [HttpPost(Name = "CreateAccount")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult<CreateAccountCommandResponse>> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var result = await commandBus.ExecuteAsync<CreateAccountCommand, CreateAccountCommandResponse>(command);

            return CreatedAtRoute("Account",
                new { AccountId = result.Id },
                result.Links);
        }

        /// <summary>
        /// Returns account by id
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">Account by id</response>
        /// <response code="404">Account not found</response>
        [HttpGet("{AccountId}", Name = "Account")]
        [ProducesResponseType(typeof(AccountQuery), 200)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<ActionResult<AccountQueryResponse>> Account([FromRoute] AccountQuery query)
        {
            var result = await queryBus.ExecuteAsync<AccountQuery, AccountQueryResponse>(query);
            return Ok(result);
        }

        /// <summary>
        /// Deletes account
        /// </summary>
        /// <response code="204">Deletes course for author</response>
        /// <response code="404">Author or course not found</response>
        [HttpDelete("{AccountId}", Name = "DeleteAccount")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<ActionResult> DeleteAuthorCourse()
        {
            await commandBus.ExecuteAsync(new DeleteAccountCommand());
            return NoContent();
        }

        #region HttpOptions
        /// <summary>
        /// Communication options for a given URL
        /// </summary>
        /// <response code="200">Returns allowed http methods</response>
        [HttpOptions(Name = "AccountsOptions")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult AccountsControllerOptions()
        {
            Response.Headers.Add("Allow", "POST, DELETE, OPTIONS");
            return Ok();
        }
        #endregion
    }
}
