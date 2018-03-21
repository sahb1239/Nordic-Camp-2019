using System.Threading.Tasks;
using GraphQL.Types;

namespace Backend.GraphQL.Helper.Schema.Base
{
    public interface IGraphQLExecutor
    {
        Task<object> Resolve(ResolveFieldContext context);
    }
}
