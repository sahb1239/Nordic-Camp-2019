using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;

namespace Backend.GraphQL.Helper.Schema
{
    public class GraphQLDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public GraphQLDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            var serviceType = _serviceProvider.GetService(type);
            if (serviceType != null)
                return serviceType;

            // Check if constructor with 0 arguments was found
            if (type.GetConstructors().Any(e => e.IsPublic && e.GetParameters().Length == 0))
            {
                return Activator.CreateInstance(type);
            }

            // Get all constructors
            var constructors = type.GetConstructors().Where(e => e.IsPublic);
            foreach (var constructor in constructors)
            {
                var arguments = new List<object>();
                var foundAll = true;
                foreach (var argument in constructor.GetParameters())
                {
                    var resolvedArgument = Resolve(argument.ParameterType);
                    if (resolvedArgument == null)
                    {
                        foundAll = false;
                        break;
                    }
                    arguments.Add(resolvedArgument);
                }

                if (foundAll)
                {
                    return Activator.CreateInstance(type, arguments.ToArray());
                }
            }

            throw new NotSupportedException("Cound not resolve constructor for type " + type);
        }
    }
}