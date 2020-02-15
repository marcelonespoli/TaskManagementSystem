using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TMS.Application.Interfaces;
using TMS.Application.ViewModels;
using TMS.Domain.Core.Notifications;
using TMS.Domain.Interfaces;

namespace TMS.Service.Api.Controllers
{
    public class TasksController : BaseController
    {
        private readonly ITaskAppService _taskAppService;

        public TasksController(
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications,
            ITaskAppService taskAppService) : base( mediator, notifications)
        {
            _taskAppService = taskAppService;
        }

        [HttpGet]
        [Route("tasks")]
        public IEnumerable<TaskViewModel> Get()
        {
            return _taskAppService.GetAll();
        }

        [HttpGet]
        [Route("tasks/{id:guid}")]
        public TaskViewModel Get(Guid id)
        {
            return _taskAppService.GetById(id);
        }

        [HttpGet]
        [Route("tasks/subtasks/{taskId:guid}")]
        public IEnumerable<SubtaskViewModel> GetSubtasksForTask(Guid taskId)
        {
            return _taskAppService.GetSubtasksByTaskId(taskId);
        }        

        [HttpPost]
        [Route("tasks")]
        public IActionResult Post([FromBody] CreateTaskViewModel taskViewModel)
        {
            if (!IsModelStateValid())
            {
                return Response();
            }

            var taskCommand = _taskAppService.CreateTask(taskViewModel);    
            return Response(taskCommand);
        }

        [HttpPut]
        [Route("tasks")]
        public IActionResult Put([FromBody] UpdateTaskViewModel taskViewModel)
        {
            if (!IsModelStateValid())
            {
                return Response();
            }

            var taskCommand = _taskAppService.UpdateTask(taskViewModel);
            return Response(taskCommand);
        }

        [HttpDelete]
        [Route("tasks/{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var taskCommand = _taskAppService.DeleteTask(id);
            return Response(taskCommand);
        }

    }
}