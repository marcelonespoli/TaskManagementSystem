using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TMS.Domain.Tasks.Events
{
    public class TaskEventHandler :
        INotificationHandler<TaskCreatedEvent>,
        INotificationHandler<TaskUpdatedEvent>,
        INotificationHandler<TaskExcludedEvent>,
        INotificationHandler<SubtaskAddedEvent>,
        INotificationHandler<SubtaskUpdatedEvent>,
        INotificationHandler<SubtaskExcludedEvent>
    {
        public Task Handle(TaskCreatedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Run some action
            return Task.CompletedTask;
        }

        public Task Handle(TaskUpdatedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Run some action
            return Task.CompletedTask;
        }

        public Task Handle(TaskExcludedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Run some action
            return Task.CompletedTask;
        }

        public Task Handle(SubtaskAddedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Run some action
            return Task.CompletedTask;
        }

        public Task Handle(SubtaskUpdatedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Run some action
            return Task.CompletedTask;
        }

        public Task Handle(SubtaskExcludedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Run some action
            return Task.CompletedTask;
        }
    }
}
