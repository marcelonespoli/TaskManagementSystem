using System;

namespace TMS.Domain.Tasks.Commands
{
    public class AddSubtaskCommand : BaseSubtaskCommand
    {
        public AddSubtaskCommand(string name, string description, Guid taskId)
        {
            var id = Guid.NewGuid();

            Id = id;
            Name = name;
            Description = description;
            State = States.Planned;
            TaskId = taskId;
            AggregateId = id;
        }
    }
}
