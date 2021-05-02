using Imagegram.Infrastructure.Query;
using System;

namespace Imagegram.Models.Queries
{
    public class PostQuery : IQuery
    {
        public Guid PostId { get; set; }
    }
}
