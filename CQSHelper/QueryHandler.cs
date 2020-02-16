﻿using CQSHelper.Interfaces;
using System.Threading.Tasks;

namespace CQSPROJ.CQSHelper
{
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery
        where TResult : IResult
    {
        public abstract Task<TResult> ExecuteAsync(TQuery query);
    }
}
