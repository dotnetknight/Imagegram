using Imagegram.Models.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace Imagegram.API.SwaggerRequestExamples
{
    public class CreateAccountCommandExample : IExamplesProvider<CreateAccountCommand>
    {
        public CreateAccountCommand GetExamples()
        {
            return new CreateAccountCommand
            {
                Name = "Niko"
            };
        }
    }
}
