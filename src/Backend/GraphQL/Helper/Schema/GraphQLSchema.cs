using GraphQL;
using GraphQL.Types;

namespace Backend.GraphQL.Helper.Schema
{
    public class GraphQLSchema<TQuery, TMutation> : global::GraphQL.Types.Schema
        where TQuery : IObjectGraphType
        where TMutation : IObjectGraphType
    {
        public GraphQLSchema(TQuery graphQLQuery, TMutation graphQLMutation, IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
            Query = graphQLQuery;
            Mutation = graphQLMutation;
        }
    }
}
