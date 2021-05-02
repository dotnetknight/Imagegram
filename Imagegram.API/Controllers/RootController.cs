using Imagegram.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Imagegram.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/vnd.marvin.hateoas+json")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RootController : ControllerBase
    {
        /// <summary>
        /// Returns root links for application
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Root")]
        public IActionResult GetRoot()
        {
            var links = new List<LinkModel>
            {
                new LinkModel(Url.Link("Root", new { }),
                "self",
                "GET"
                ),

                new LinkModel(Url.Link("CreateAccount", new { }),
                "create_account",
                "POST"
                ),

                new LinkModel(Url.Link("DeleteAccount", new { AccountId = Guid.Empty}),
                "delete_account",
                "DELETE"
                ),

                new LinkModel(Url.Link("Posts", new { }),
                "user_posts",
                "GET"
                ),

                new LinkModel(Url.Link("Post", new { PostId = Guid.Empty }),
                "get_post",
                "GET"
                ),

                new LinkModel(Url.Link("CreatePost", new { }),
                "create_post",
                "POST"
                ),

                new LinkModel(Url.Link("Comment", new { PostId = Guid.Empty }),
                "create_comment_to_post",
                "POST"
                ),

                new LinkModel(Url.Link("Comments", new { PostId = Guid.Empty }),
                "get_comments_to_post",
                "GET"
                ),

                new LinkModel(Url.Link("AccountsOptions", new { }),
                "accounts_controller_options",
                "OPTIONS"
                ),

                new LinkModel(Url.Link("PostsOptions", new { }),
                "posts_controller_options",
                "OPTIONS"
                ),
            };

            return Ok(links);
        }
    }
}
