using System;
using System.Linq;
using System.Reflection;
using Backend.GraphQL.Helper.Schema;
using Backend.GraphQL.Helper.Schema.Base;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.GraphQL.Helper.Builder
{
    public static class GraphQLServiceBuilder
    {
        public static IServiceCollection RegistrerSchema<TQuery, TMutation>(this IServiceCollection services)
            where TQuery : class, IObjectGraphType
            where TMutation : class, IObjectGraphType
        {
            // Registrer Schema, Query and Mutation
            services
                .AddSingleton<GraphQLSchema<TQuery, TMutation>>()
                .AddSingleton<TQuery>()
                .AddSingleton<TMutation>();

            // Dependency resolver
            services.AddSingleton<IDependencyResolver, GraphQLDependencyResolver>();

            // Registrer Queries
            foreach (var type in typeof(TQuery).GetTypeInfo().Assembly.GetTypes()
                .Where(type => !type.GetTypeInfo().IsAbstract && type.GetTypeInfo().IsSubclassOf(typeof(GraphQLBaseInformation))))
            {
                // Add query base
                services.AddSingleton(type);
                services.AddSingleton<GraphQLBaseInformation>(provider =>
                    (GraphQLBaseInformation) provider.GetService(type));

                // Get mutationInformation
                if (IsSubclassOfRawGeneric(typeof(GraphQLBase<,>), type))
                {
                    // Get generic type
                    Type queryBaseType = type;
                    while ((!queryBaseType.GetTypeInfo().IsGenericType || queryBaseType.GetGenericTypeDefinition() != typeof(GraphQLBase<,>)) && queryBaseType != typeof(object))
                    {
                        queryBaseType = queryBaseType.GetTypeInfo().BaseType;
                    }

                    // Check if it's a object
                    if (queryBaseType == typeof(object))
                        continue;

                    // Add executor to services
                    var executor = queryBaseType.GenericTypeArguments[1];
                    services.AddScoped(executor);
                }
            }
            
            return services;
        }

        static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.GetTypeInfo().IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.GetTypeInfo().BaseType;
            }
            return false;
        }
    }
}
