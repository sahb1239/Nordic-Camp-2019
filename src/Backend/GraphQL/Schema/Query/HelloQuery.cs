using System;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Schema;
using Backend.GraphQL.Helper.Schema.Base;
using GraphQL.Types;

namespace Backend.GraphQL.Schema.Query
{
    [RegistrerQuery]
    public class HelloQuery : GraphQLBase<StringGraphType, HelloQuery.HelloQueryExecutor>
    {
        public HelloQuery(IServiceProvider serviceProvider) : base(serviceProvider, "hello")
        {
        }

        public class HelloQueryExecutor : IGraphQLExecutor
        {
            public Task<object> Resolve(ResolveFieldContext context)
            {
                return Task.FromResult<object>("query");
            }
        }
    }
}
