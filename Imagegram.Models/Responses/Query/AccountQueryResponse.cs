using Imagegram.Models.DTOs;

namespace Imagegram.Models.Responses.Query
{
    public class AccountQueryResponse : BaseResponse
    {
        public AccountDto Account { get; set; }
    }
}
