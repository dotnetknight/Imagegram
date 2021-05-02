using Imagegram.Models.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;

namespace Imagegram.Services
{
    public class HateoasLinksService
    {

        #region Properties

        private readonly IUrlHelperFactory urlHelperFactory;
        private readonly IActionContextAccessor actionContextAccessor;

        #endregion

        #region Constructor

        public HateoasLinksService(
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            this.urlHelperFactory = urlHelperFactory;
            this.actionContextAccessor = actionContextAccessor;
        }

        #endregion

        public IEnumerable<LinkModel> CreateLinksForImagegram(Guid id)
        {
            var urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);

            var links = new List<LinkModel>
            {

                new LinkModel(urlHelper.Link("CreateAccount", new { }),
                "create_account",
                "POST"
                ),

                new LinkModel(urlHelper.Link("DeleteAccount", new { AccountId = id }),
                "delete_account",
                "DELETE"
                ),

                new LinkModel(urlHelper.Link("Posts", new { }),
                "user_posts",
                "GET"
                ),

                new LinkModel(urlHelper.Link("Post", new { PostId = id }),
                "get_post",
                "GET"
                ),

                new LinkModel(urlHelper.Link("CreatePost", new { }),
                "create_post",
                "POST"
                ),

                new LinkModel(urlHelper.Link("Comment", new { PostId = id }),
                "create_comment_to_post",
                "POST"
                ),
                
                new LinkModel(urlHelper.Link("Comments", new { PostId = id }),
                "get_comments_to_post",
                "GET"
                ),
            };

            return links;
        }
    }
}
