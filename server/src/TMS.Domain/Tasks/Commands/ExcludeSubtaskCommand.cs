using System;

namespace TMS.Domain.Tasks.Commands
{
    public class ExcludeSubtaskCommand : BaseTaskCommand
    {
        public ExcludeSubtaskCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
