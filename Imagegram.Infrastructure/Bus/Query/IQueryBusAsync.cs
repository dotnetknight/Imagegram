using Imagegram.Infrastructure.Query;
using System.Threading.Tasks;

namespace Imagegram.Infrastructure.Bus.Query
{
    public interface IQueryBusAsync
    {
        Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }
}
