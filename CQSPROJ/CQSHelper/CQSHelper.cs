﻿using CQSHelper.Interfaces;
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
        /// Extention method for scanning assmebly and auto registering any handlers created. <see cref="IQueryHandler{TQuery, TResult}"/>, <see cref="ICommandHandler{TCommand}"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <param name="lifetime"></param>
        public static void RegisterCQS(this IServiceCollection services, Assembly[] assemblies = null, ServiceLifetime lifetime = ServiceLifetime.Transient) {

            services.Add(new ServiceDescriptor(typeof(IDispatcher), typeof(Dispatcher), lifetime));

            // if no assemblies passed through, default to just searching current assembly
            var assembliesToSearch = assemblies == null ? new[] { typeof(Dispatcher).Assembly } : assemblies;

            // get types in defined assemblies where that implements ICommand or IQuery and IS NOT abstract
            var typesFromAssemblies = assembliesToSearch.SelectMany(a => a.DefinedTypes.Where(t => t.ImplementedInterfaces.Any(i => (i.Name == typeof(IQueryHandler<,>).Name) || i.Name == typeof(ICommandHandler<>).Name) && !t.IsAbstract));

            // add any handlers found
            foreach (var type in typesFromAssemblies)
            {
                if (type.ImplementedInterfaces.Any(i => i.Name == typeof(IQueryHandler<,>).Name))
                {
                    services.Add(new ServiceDescriptor(typeof(IQueryHandler<,>), type, lifetime));
                } else if (type.ImplementedInterfaces.Any(i => i.Name == typeof(ICommandHandler<>).Name)) {
                    services.Add(new ServiceDescriptor(typeof(ICommandHandler<>), type, lifetime));
                }
            }
        }
    }
}
