using System;
using System.Collections.Generic;

namespace TMS.Domain.Tasks.Commands
{
    public class CreateTaskCommand : BaseTaskCommand
    {
        public ICollection<AddSubtaskCommand> Subtasks { get; private set; }

        public CreateTaskCommand(string name, string description, DateTime? startDate, DateTime? finishDate, 
            ICollection<AddSubtaskCommand> subtasks)
        {
            var id = Guid.NewGuid();

            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            FinishDate = finishDate;
            State = States.Planned;
            Subtasks = subtasks;
            AggregateId = id;
        }
    }
}
