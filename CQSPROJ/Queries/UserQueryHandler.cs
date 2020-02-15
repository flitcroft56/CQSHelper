using CQSHelper.Interfaces;
using CQSPROJ.CQSHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSPROJ.Queries
{
    public class UserQueryHandler<TQuery, TResult> : QueryHandler<GetUserQuery, GetUserQueryResult>
    {
        public override async Task<GetUserQueryResult> ExecuteAsync(GetUserQuery query)
        {
            GetUserQueryResult _result = new GetUserQueryResult()
            {
                Name = query.Name,
                Colour = query.Colour,
                BaloonAnimal = "panda"
            };
            return  _result;
        }
    }
}
