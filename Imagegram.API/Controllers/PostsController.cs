using Imagegram.Infrastructure.Bus.Command;
using Imagegram.Infrastructure.Bus.Query;
using Imagegram.Models.Commands;
using Imagegram.Models.Models;
using Imagegram.Models.Queries;
using Imagegram.Models.ResourceParameters;
using Imagegram.Models.Responses.Command;
using Imagegram.Models.Responses.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Imagegram.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IQueryBusAsync queryBus;
        private readonly ICommandBusAsync commandBus;

        public PostsController(
            IQueryBusAsync queryBus,
            ICommandBusAsync commandBus)
        {
            this.queryBus = queryBus;
            this.commandBus = commandBus;
        }

        /// <summary>
        /// Returns all posts from all users along with last 3 comments to each post
        /// </summary>
        /// <param name="postsResourceParameters"></param>
        /// <response code="200">All posts from all users along with last 3 comments to each post</response>
        /// <response code="404">Posts not found</response>
        [HttpGet(Name = "Posts")]
        [ProducesResponseType(typeof(PostQueryResponse), 200)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<ActionResult<PostQueryResponse>> Posts([FromQuery] PostsResourceParameters postsResourceParameters)
        {
            var result = await queryBus.ExecuteAsync<PostsQuery, PostsQueryResponse>(new PostsQuery(postsResourceParameters));
            return Ok(result);
        }

        /// <summary>
        /// Returns post by id
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">Post with id</response>
        /// <response code="404">Post not found</response>
        [HttpGet("{PostId}", Name = "Post")]
        [ProducesResponseType(typeof(PostQueryResponse), 200)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<ActionResult<PostQueryResponse>> Post([FromRoute] PostQuery query)
        {
            var result = await queryBus.ExecuteAsync<PostQuery, PostQueryResponse>(query);
            return Ok(result);
        }

        /// <summary>
        /// Creates new post in the system
        /// </summary>
        /// <param name="command"></param>
        /// <response code="201">Creates new post in the system</response>
        /// <response code="400">Unable to add new user in the system due to validation error</response>
        /// <response code="404">Account not found</response>
        [HttpPost(Name = "CreatePost")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<CreatePostCommandResponse>> CreatePost([FromForm] CreatePostCommand command)
        {
            var result = await commandBus.ExecuteAsync<CreatePostCommand, CreatePostCommandResponse>(command);

            return CreatedAtRoute("Post",
                new { PostId = result.Id },
                result.Links);
        }

        #region HttpOptions

        /// <summary>
        /// Communication options for a given URL
        /// </summary>
        /// <response code="200">Returns allowed http methods</response>
        [HttpOptions(Name = "PostsOptions")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult AccountsControllerOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, OPTIONS");
            return Ok();
        }
        #endregion
    }
}
