using System;
using System.Collections.Generic;

namespace TMS.Domain.Tasks.Events
{
    public class TaskUpdatedEvent : BaseTaskEvent
    {
        public TaskUpdatedEvent(Guid id, string name, string description, DateTime? startDate, DateTime? finishDate, int state)
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
