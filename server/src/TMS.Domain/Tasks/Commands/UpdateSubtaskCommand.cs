using System;

namespace TMS.Domain.Tasks.Commands
{
    public class UpdateSubtaskCommand : BaseSubtaskCommand
    {
        public UpdateSubtaskCommand(Guid id, string name, string description, States state, Guid taskId)
        {
            Id = id;
            Name = name;
            Description = description;
            State = state;
            TaskId = taskId;
            AggregateId = id;
        }
    }
}
