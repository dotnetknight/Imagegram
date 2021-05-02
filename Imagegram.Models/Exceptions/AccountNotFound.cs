namespace Imagegram.Models.Exceptions
{
    public class AccountNotFound : BaseApiException
    {
        public override string Message { get; }

        public AccountNotFound(string message) : base(System.Net.HttpStatusCode.NotFound)
        {
            Message = message;
        }
    }
}
