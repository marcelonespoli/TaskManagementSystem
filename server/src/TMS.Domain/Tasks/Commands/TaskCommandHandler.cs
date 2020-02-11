using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMS.Domain.Core.Notifications;
using TMS.Domain.Handlers;
using TMS.Domain.Interfaces;
using TMS.Domain.Tasks.Events;
using TMS.Domain.Tasks.Repository;

namespace TMS.Domain.Tasks.Commands
{
    public class TaskCommandHandler : CommandHandler,
        IRequestHandler<CreateTaskCommand, bool>,
        IRequestHandler<UpdateTaskCommand, bool>,
        IRequestHandler<ExcludeTaskCommand, bool>,
        IRequestHandler<AddSubtaskCommand, bool>,
        IRequestHandler<UpdateSubtaskCommand, bool>,
        IRequestHandler<ExcludeSubtaskCommand, bool>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMediatorHandler _mediator;

        public TaskCommandHandler(
            IUnitOfWork uow, 
            IMediatorHandler mediator,
            ITaskRepository taskRepository,
            INotificationHandler<DomainNotification> notifications) : base(uow, mediator, notifications)
        {
            _mediator = mediator;
            _taskRepository = taskRepository;
        }

        public Task<bool> Handle(CreateTaskCommand message, CancellationToken cancellationToken)
        {            
            var task = new TaskData(message.Id, message.Name, message.Description,
                message.StartDate, message.FinishDate, message.State);

            if (message.Subtasks != null)
            {
                foreach (var msgSubtask in message.Subtasks)
                {
                    var subtask = new Subtask(msgSubtask.Id, msgSubtask.Name, msgSubtask.Description, msgSubtask.State, task.Id);
                    task.Subtasks.Add(subtask);
                }
            }

            if (!IsTaskValid(task)) 
                return Task.FromResult(false);

            _taskRepository.Add(task);

            if (Commit())
            {
                var subtasks = new List<SubtaskAddedEvent>();
                foreach (var sub in task.Subtasks)
                {
                    var subtask = new SubtaskAddedEvent(sub.Id, sub.Name, sub.Description, sub.State, sub.TaskId);
                    subtasks.Add(subtask);
                }
                _mediator.PublishEvent(new TaskCreatedEvent(task.Id, task.Name, task.Description,
                            task.StartDate, task.FinishDate, task.State, subtasks));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateTaskCommand message, CancellationToken cancellationToken)
        {
            var state = message.State;
            var messageSubtasks = _taskRepository.GetSubtasksByTaskId(message.Id);  
            if (messageSubtasks.Any())
            {
                state = GetTaskStateBasedOnSubtasks(messageSubtasks);
            }

            var task = new TaskData(message.Id, message.Name, message.Description,
                message.StartDate, message.FinishDate, state);

            if (!IsTaskValid(task))
                return Task.FromResult(false);            

            _taskRepository.Update(task);

            if (Commit())
            {
                _mediator.PublishEvent(new TaskUpdatedEvent(task.Id, task.Name, task.Description,
                            task.StartDate, task.FinishDate, task.State));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(ExcludeTaskCommand message, CancellationToken cancellationToken)
        {
            if (!CanTaskBeExclude(message.Id, message.MessageType))
                return Task.FromResult(false);

            _taskRepository.Remove(message.Id);

            if (Commit())
            {
                _mediator.PublishEvent(new TaskExcludedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(AddSubtaskCommand message, CancellationToken cancellationToken)
        {
            var subtask = new Subtask(message.Id, message.Name, message.Description, message.State, message.TaskId);

            if (!IsSubtaskValid(subtask))
                return Task.FromResult(false);

            _taskRepository.AddSubtask(subtask);

            if (Commit())
            {
                _mediator.PublishEvent(new SubtaskAddedEvent(subtask.Id, subtask.Name, subtask.Description,
                            subtask.State, message.TaskId));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateSubtaskCommand message, CancellationToken cancellationToken)
        {
            var subtask = new Subtask(message.Id, message.Name, message.Description, message.State, message.TaskId);

            if (!IsSubtaskValid(subtask))
                return Task.FromResult(false);

            _taskRepository.UpdateSubtask(subtask);

            if (Commit())
            {
                _mediator.PublishEvent(new SubtaskUpdatedEvent(subtask.Id, subtask.Name, subtask.Description,
                            subtask.State, message.TaskId));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(ExcludeSubtaskCommand message, CancellationToken cancellationToken)
        {
            if (!SubtaskExists(message.Id, message.MessageType))
                return Task.FromResult(false);

            _taskRepository.RemoveSubtask(message.Id);

            if (Commit())
            {
                _mediator.PublishEvent(new TaskExcludedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        private bool IsTaskValid(TaskData task)
        {
            if (task.IsValid()) return true;

            NotifyValidationError(task.ValidationResult);
            return false;
        }

        private bool IsSubtaskValid(Subtask subtask)
        {
            if (subtask.IsValid()) return true;

            NotifyValidationError(subtask.ValidationResult);
            return false;
        }

        private bool CanTaskBeExclude(Guid id, string messageType)
        {
            var task = _taskRepository.GetById(id);
            if (task == null)
            {
                _mediator.PublishEvent(new DomainNotification(messageType, "Task not found"));
                return false;
            }

            var subtesks = _taskRepository.GetSubtasksByTaskId(id);
            if (subtesks.Any())
            {
                _mediator.PublishEvent(new DomainNotification(messageType, "To be excluded, the Task must have no subtasks"));
                return false;
            }

            return true;
        }

        private bool SubtaskExists(Guid id, string messageType)
        {
            var subtask = _taskRepository.GetSubtaskById(id);

            if (subtask != null) return true;

            _mediator.PublishEvent(new DomainNotification(messageType, "Subtask not found"));
            return false;
        }

        private States GetTaskStateBasedOnSubtasks(IEnumerable<Subtask> subtasks)
        {
            var isAllTasksCompleted = subtasks.Count(s => s.State == (int)States.Completed) == subtasks.Count();
            var isInProgress = subtasks.Any(s => s.State == (int)States.InProgress);

            if (isAllTasksCompleted)
                return States.Completed;

            if (isInProgress)
                return States.InProgress;

            return States.Planned;
        }
    }
}
