using Imagegram.Infrastructure.Query;
using System;

namespace Imagegram.Models.Queries
{
    public class AccountQuery : IQuery
    {
        public Guid AccountId { get; set; }
    }
}
