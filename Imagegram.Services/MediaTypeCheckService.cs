using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net.Http.Headers;
namespace Imagegram.Services
{
    public class MediaTypeCheckService
    {
        #region Properties

        private readonly IActionContextAccessor actionContextAccessor;

        #endregion

        #region Constructor

        public MediaTypeCheckService(IActionContextAccessor actionContextAccessor)
        {
            this.actionContextAccessor = actionContextAccessor;
        }

        #endregion

        public bool ShouldSendHateoasLinks()
        {
            if (MediaTypeHeaderValue.TryParse(actionContextAccessor.ActionContext.HttpContext.Request.Headers["Accept"], out MediaTypeHeaderValue parsedMediaType))
            {
                if (parsedMediaType.MediaType == "application/vnd.marvin.hateoas+json")
                    return true;
            }

            return false;
        }
    }
}
