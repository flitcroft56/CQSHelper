using System.Threading.Tasks;

namespace CQSHelper.Interfaces
{
    public interface ICommandHandler <TCommand>
    {
        public Task ExecuteAsync(TCommand command);
    }
}
