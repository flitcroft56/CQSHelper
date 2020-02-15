using CQSPROJ.CQSHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSPROJ.Commands
{
    public class UserUpdateCommandHandler<TCommand> : CommandHandler<UserUpdateCommand>
    {
        public override async Task ExecuteAsync(UserUpdateCommand command)
        {
            var testing = command.Name;
        }
    }
}
