using System;
using System.Collections.Generic;
using TMS.Application.ViewModels;
using TMS.Domain.Tasks.Commands;

namespace TMS.Application.Interfaces
{
    public interface ITaskAppService
    {
        IEnumerable<TaskViewModel> GetAll();
        TaskViewModel GetById(Guid id);         
        CreateTaskCommand CreateTask(CreateTaskViewModel taskViewModel);
        UpdateTaskCommand UpdateTask(UpdateTaskViewModel taskViewModel);
        ExcludeTaskCommand DeleteTask(Guid id);
        SubtaskViewModel GetSubtaskById(Guid id);
        IEnumerable<SubtaskViewModel> GetSubtasksByTaskId(Guid taskId);
        AddSubtaskCommand AddSubtask(AddSubtaskViewModel subtaskViewModel);
        UpdateSubtaskCommand UpdateSubtask(SubtaskViewModel subtaskViewModel);
        ExcludeSubtaskCommand DeleteSubtask(Guid id);
    }
}
