using CQSHelper.Interfaces;
using System.Threading.Tasks;

namespace CQSPROJ.CQSHelper
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        public abstract Task ExecuteAsync(TCommand command);
    }
}
