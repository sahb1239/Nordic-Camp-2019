using System;
using System.Threading.Tasks;
using Backend.GraphQL.Helper.Schema;
using Backend.GraphQL.Helper.Schema.Base;
using GraphQL.Types;

namespace Backend.GraphQL.Schema.Mutation
{
    [RegistrerMutation]
    public class HelloMutation : GraphQLBase<StringGraphType, HelloMutation.HelloMutationExecutor>
    {
        public HelloMutation(IServiceProvider serviceProvider) : base(serviceProvider, "hello")
        {
        }

        public class HelloMutationExecutor : IGraphQLExecutor
        {
            public Task<object> Resolve(ResolveFieldContext context)
            {
                return Task.FromResult<object>("mutation");
            }
        }
    }
}
