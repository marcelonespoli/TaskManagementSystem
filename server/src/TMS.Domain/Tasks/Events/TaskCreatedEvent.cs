using System;
using System.Collections.Generic;

namespace TMS.Domain.Tasks.Events
{
    public class TaskCreatedEvent : BaseTaskEvent
    {        
        public ICollection<SubtaskAddedEvent> Subtasks { get; private set; }

        public TaskCreatedEvent(Guid id, string name, string description, DateTime? startDate, DateTime? finishDate,
            int state, ICollection<SubtaskAddedEvent> subtasks)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            FinishDate = finishDate;
            State = state;
            Subtasks = subtasks;
            AggregateId = id;
        }
    }
}
