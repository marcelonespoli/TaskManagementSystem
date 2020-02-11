using System;

namespace TMS.Domain.Tasks.Events
{
    public class SubtaskUpdatedEvent : BaseSubtaskEvent
    {
        public SubtaskUpdatedEvent(Guid id, string name, string description, int state, Guid taskId)
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
