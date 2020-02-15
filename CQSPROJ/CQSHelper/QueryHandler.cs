using CQSHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSPROJ.CQSHelper
{
    public class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery
        where TResult : IResult
    {
        public Task<TResult> ExecuteAsync(TQuery query)
        {
            throw new Exception("Dick trapped in lift door");
        }
    }
}
