namespace Imagegram.Models.Exceptions
{
    public class PostNotFound : BaseApiException
    {
        public override string Message { get; }

        public PostNotFound(string message) : base(System.Net.HttpStatusCode.NotFound)
        {
            Message = message;
        }
    }
}
