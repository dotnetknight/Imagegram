using Imagegram.Infrastructure.Query;
using System.Threading.Tasks;

namespace Imagegram.Infrastructure.Bus.Query
{
    public interface IQueryHandlerAsync<TQuery, TResult> where TQuery : IQuery
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
