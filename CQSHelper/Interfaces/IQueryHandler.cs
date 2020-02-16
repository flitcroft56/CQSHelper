﻿using System.Threading.Tasks;

namespace CQSHelper.Interfaces
{
    public interface IQueryHandler <in TQuery, TResult>
        where TQuery : IQuery
        where TResult : IResult
    {
        public Task<TResult> ExecuteAsync(TQuery query);
    }
}
