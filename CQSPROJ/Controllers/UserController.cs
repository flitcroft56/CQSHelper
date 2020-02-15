using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQSHelper.Interfaces;
using CQSPROJ.Commands;
using CQSPROJ.CQSHelper;
using CQSPROJ.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CQSPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public UserController(IDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<GetUserQueryResult> Get(GetUserQuery query) => 
            await _dispatcher.DispatchAsync<GetUserQuery, GetUserQueryResult>(query).ConfigureAwait(false);


        [HttpPost]
        public async Task Post(UserUpdateCommand command) =>
            await _dispatcher.DispatchAsync(command).ConfigureAwait(false);
    }
}
