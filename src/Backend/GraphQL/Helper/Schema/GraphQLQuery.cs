using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Backend.GraphQL.Helper.Schema.Base;
using GraphQL.Types;

namespace Backend.GraphQL.Helper.Schema
{
    public class GraphQLQuery : ObjectGraphType<object>
    {
        public GraphQLQuery(IEnumerable<GraphQLBaseInformation> fields)
        {
            foreach (var field in fields)
            {
                if (field.GetType().GetTypeInfo().GetCustomAttributes<RegistrerQueryAttribute>().Any())
                {
                    this.AddField(field);
                }
            }
        }
    }
}
