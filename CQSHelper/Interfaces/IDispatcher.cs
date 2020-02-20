using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSHelper.Interfaces
{
    public interface IDispatcher
    {
        public Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery
            where TResult : IResult;

        public Task DispatchAsync<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
