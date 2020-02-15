using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using TMS.Application.Interfaces;
using TMS.Application.ViewModels;
using TMS.Domain.Core.Notifications;
using TMS.Domain.Interfaces;

namespace TMS.Service.Api.Controllers
{
    [Route("v1")]
    [ApiController]
    public class SubtasksController : BaseController
    {
        private readonly ITaskAppService _taskAppService;

        public SubtasksController(
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications,
            ITaskAppService taskAppService) : base(mediator, notifications)
        {
            _taskAppService = taskAppService;
        }

        [HttpGet]
        [Route("subtasks/{id:guid}")]
        public SubtaskViewModel Get(Guid id)
        {
            return _taskAppService.GetSubtaskById(id);
        }

        [HttpPost]
        [Route("subtasks")]
        public IActionResult Post([FromBody] AddSubtaskViewModel subtaskViewModel)
        {
            if (!IsModelStateValid())
            {
                return Response();
            }

            var subtaskCommand = _taskAppService.AddSubtask(subtaskViewModel);
            return Response(subtaskCommand);
        }

        [HttpPut]
        [Route("subtasks")]
        public IActionResult Put([FromBody] SubtaskViewModel subtaskViewModel)
        {
            if (!IsModelStateValid())
            {
                return Response();
            }

            var subtaskCommand = _taskAppService.UpdateSubtask(subtaskViewModel);
            return Response(subtaskCommand);
        }

        [HttpDelete]
        [Route("subtasks/{id:guid}")]
        public IActionResult DeletSubtask(Guid id)
        {
            var subtaskCommand = _taskAppService.DeleteSubtask(id);
            return Response(subtaskCommand);
        }

    }
}