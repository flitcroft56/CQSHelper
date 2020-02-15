using CQSHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSPROJ.CQSHelper
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        public abstract Task ExecuteAsync(TCommand command);
    }
}
