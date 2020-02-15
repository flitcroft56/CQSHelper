using CQSHelper.Interfaces;
using CQSPROJ.CQSHelper;
using CQSPROJ.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CQSHelper.CQSHelper
{
    public static class CQSHelper
    {

        /// <summary>
        /// Extention method for registering CQS types with the .net core dependency injection container
        /// </summary>
        /// <param name="services">Services to extend</param>
        /// <param name="lifttime">Service life time of depenency</param>
        public static void RegisterCQSTypes(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) {

            services.Add(new ServiceDescriptor(typeof(IQuery), typeof(Query), lifetime));
            services.Add(new ServiceDescriptor(typeof(IResult), typeof(Result), lifetime));
            services.Add(new ServiceDescriptor(typeof(IDispatcher), typeof(Dispatcher), lifetime));
        }

        /// <summary>
        /// Extention method for scanning assmebly and auto registering any handlers created. <see cref="IQueryHandler{TQuery, TResult}"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <param name="lifetime"></param>
        public static void AutoRegisterCQSHandlers(this IServiceCollection services, Assembly[] assemblies = null, ServiceLifetime lifetime = ServiceLifetime.Transient) {

            // if no assemblies passed through, default to just searching current assembly
            var assembliesToSearch = assemblies == null ? new[] { typeof(Dispatcher).Assembly } : assemblies;

            var typesFromAssemblies = assembliesToSearch.SelectMany(a => a.DefinedTypes.Where(t => t.ImplementedInterfaces.Any(i => i.Name == typeof(IQueryHandler<IQuery, IResult>).Name)));

            // add any handlers found
            foreach (var type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(typeof(IQueryHandler<,>), type, lifetime));
            }
        }
    }
}
