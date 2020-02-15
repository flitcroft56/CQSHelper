using System;
using System.Threading.Tasks;
using CQSHelper.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CQSHelper
{
    /// <summary>
    /// Class for dispatching commands and queries (CQS Pattern)
    /// </summary>
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Dispatch a query asynchronously 
        /// </summary>
        /// <typeparam name="TQuery">Query type to run</typeparam>
        /// <typeparam name="TResult">Expected result type to return</typeparam>
        /// <param name="query">Query instance to run</param>
        /// <returns></returns>
        public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : IResult
        {
            var queryHandler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();

            if (queryHandler == null)
                throw new TypeLoadException($"No handler found for type: {query.GetType()}");

            return await queryHandler.ExecuteAsync(query);

        }
    }
}
