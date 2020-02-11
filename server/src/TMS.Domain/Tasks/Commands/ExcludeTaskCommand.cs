using System;

namespace TMS.Domain.Tasks.Commands
{
    public class ExcludeTaskCommand : BaseTaskCommand
    {
        public ExcludeTaskCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
