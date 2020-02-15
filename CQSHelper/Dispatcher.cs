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
                throw new TypeLoadException($"No handler found for query type: {query.GetType()}");

            return await queryHandler.ExecuteAsync(query);

        }

        /// <summary>
        /// Dispatch a command asynchronously
        /// </summary>
        /// <typeparam name="TCommand">Command type to run</typeparam>
        /// <param name="command">Command instance to run</param>
        /// <returns></returns>
        public async Task DispatchAsync<TCommand>(TCommand command) 
            where TCommand : ICommand
        {
            var commandHandler = _serviceProvider.GetService <ICommandHandler<TCommand>>();

            if (commandHandler == null)
                throw new TypeLoadException($"No handler found for command type: {command.GetType()}");

            await commandHandler.ExecuteAsync(command);
        }
    }
}
