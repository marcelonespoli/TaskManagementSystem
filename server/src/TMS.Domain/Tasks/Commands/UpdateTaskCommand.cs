using System;

namespace TMS.Domain.Tasks.Commands
{
    public class UpdateTaskCommand : BaseTaskCommand
    {
        public UpdateTaskCommand(Guid id, string name, string description, DateTime? startDate, DateTime? finishDate, States state)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            FinishDate = finishDate;
            State = state;
            AggregateId = id;
        }
    }
}
