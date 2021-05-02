using Imagegram.Infrastructure.Bus.Command;
using Imagegram.Infrastructure.Bus.Query;
using Imagegram.Models.Commands;
using Imagegram.Models.Queries;
using Imagegram.Models.Responses.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Imagegram.API.Controllers
{
    [Route("api/Posts/{PostId}/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IQueryBusAsync queryBus;
        private readonly ICommandBusAsync commandBus;

        public CommentsController(
            IQueryBusAsync queryBus,
            ICommandBusAsync commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        /// <summary>
        /// Creates new comment on a post
        /// </summary>
        /// <param name="command"></param>
        /// <response code="204">Creates new comment on a post</response>
        /// <response code="400">Unable to comment on a post due to validation error</response>
        /// <response code="404">Post or account with this id not found</response>
        [HttpPost(Name = "Comment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult> Comment([FromBody] CreateCommentOnPostCommand command)
        {
            await commandBus.ExecuteAsync(command);

            return NoContent();
        }

        /// <summary>
        /// Returns comments to a post sorted by date (desc)
        /// </summary>
        /// <response code="200">Comments to a post sorted by date (desc)</response>
        /// <response code="404">Posts not found</response>
        [HttpGet(Name = "Comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<ActionResult> Comments()
        {
            var result = await queryBus.ExecuteAsync<CommentsQuery, CommentsQueryResponse>(new CommentsQuery());
            return Ok(result);
        }
    }
}
