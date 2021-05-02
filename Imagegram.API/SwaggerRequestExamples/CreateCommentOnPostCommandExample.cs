using Imagegram.Models.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace Imagegram.API.SwaggerRequestExamples
{
    public class CreateCommentOnPostCommandExample : IExamplesProvider<CreateCommentOnPostCommand>
    {
        public CreateCommentOnPostCommand GetExamples()
        {
            return new CreateCommentOnPostCommand
            {
                Content = "Sample comment"
            };
        }
    }
}
