using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Backend.GraphQL.Helper.Schema.Base;
using GraphQL.Types;

namespace Backend.GraphQL.Helper.Schema
{
    public class GraphQLMutation : ObjectGraphType<object>
    {
        public GraphQLMutation(IEnumerable<GraphQLBaseInformation> fields)
        {
            foreach (var field in fields)
            {
                if (field.GetType().GetTypeInfo().GetCustomAttributes<RegistrerMutationAttribute>().Any())
                {
                    this.AddField(field);
                }
            }
        }
    }
}
