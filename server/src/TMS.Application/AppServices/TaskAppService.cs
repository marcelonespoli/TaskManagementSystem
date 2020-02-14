using AutoMapper;
using System;
using System.Collections.Generic;
using TMS.Application.Interfaces;
using TMS.Application.ViewModels;
using TMS.Domain.Interfaces;
using TMS.Domain.Tasks.Commands;
using TMS.Domain.Tasks.Repository;

namespace TMS.Application.AppServices
{
    public class TaskAppService : ITaskAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ITaskRepository _taskRepository;

        public TaskAppService(
            IMapper mapper,
            IMediatorHandler mediatorHandler,
            ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;            
            _mediatorHandler = mediatorHandler;
        }

        public IEnumerable<TaskViewModel> GetAll()
        {
            var tasks = _taskRepository.GetAll();
            return _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
        }

        public TaskViewModel GetById(Guid id)
        {
            var task = _taskRepository.GetById(id);
            return _mapper.Map<TaskViewModel>(task);
        }

        public CreateTaskCommand CreateTask(TaskViewModel taskViewModel)
        {
            var task = _mapper.Map<CreateTaskCommand>(taskViewModel);

            _mediatorHandler.SendCommand(task);

            return task;
        }

        public UpdateTaskCommand UpdateTask(TaskViewModel taskViewModel)
        {
            var task = _mapper.Map<UpdateTaskCommand>(taskViewModel);

            _mediatorHandler.SendCommand(task);

            return task;
        }

        public ExcludeTaskCommand DeleteTask(Guid id)
        {
            var taskViewModel = new TaskViewModel { Id = id };
            var task = _mapper.Map<ExcludeTaskCommand>(taskViewModel);

            _mediatorHandler.SendCommand(task);

            return task;
        }

        public SubtaskViewModel GetSubtaskById(Guid id)
        {
            var subtask = _taskRepository.GetSubtaskById(id);
            return _mapper.Map<SubtaskViewModel>(subtask);
        }

        public IEnumerable<TaskViewModel> GetSubtasksByTaskId(Guid taskId)
        {
            var subtasks = _taskRepository.GetSubtasksByTaskId(taskId);
            return _mapper.Map<IEnumerable<TaskViewModel>>(subtasks);
        }

        public AddSubtaskCommand AddSubtask(SubtaskViewModel subtaskViewModel)
        {
            var subtask = _mapper.Map<AddSubtaskCommand>(subtaskViewModel);
            _mediatorHandler.SendCommand(subtask);

            return subtask;
        }

        public UpdateSubtaskCommand UpdateSubtask(SubtaskViewModel subtaskViewModel)
        {
            var subtask = _mapper.Map<UpdateSubtaskCommand>(subtaskViewModel);
            _mediatorHandler.SendCommand(subtask);

            return subtask;
        }

        public ExcludeSubtaskCommand DeleteSubtask(Guid id)
        {
            var subtaskViewModel = new SubtaskViewModel { Id = id };
            var subtask = _mapper.Map<ExcludeSubtaskCommand>(subtaskViewModel);

            _mediatorHandler.SendCommand(subtask);

            return subtask;
        }
        
    }
}
