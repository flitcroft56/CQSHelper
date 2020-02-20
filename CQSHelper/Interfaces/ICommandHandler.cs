using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQSHelper.Interfaces
{
    public interface ICommandHandler <TCommand>
    {
        public Task ExecuteAsync(TCommand command);
    }
}
