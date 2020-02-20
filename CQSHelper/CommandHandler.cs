using CQSHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSHelper
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> 
        where TCommand : ICommand
    {
        public abstract Task ExecuteAsync(TCommand command);
    }
}
