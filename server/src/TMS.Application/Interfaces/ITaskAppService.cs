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
        CreateTaskCommand CreateTask(TaskViewModel taskViewModel);
        UpdateTaskCommand UpdateTask(TaskViewModel taskViewModel);
        ExcludeTaskCommand DeleteTask(Guid id);
        SubtaskViewModel GetSubtaskById(Guid id);
        IEnumerable<TaskViewModel> GetSubtasksByTaskId(Guid taskId);
        AddSubtaskCommand AddSubtask(SubtaskViewModel subtaskViewModel);
        UpdateSubtaskCommand UpdateSubtask(SubtaskViewModel subtaskViewModel);
        ExcludeSubtaskCommand DeleteSubtask(Guid id);
    }
}
