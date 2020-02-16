using Microsoft.AspNetCore.Mvc;
using System;
using TMS.Application.Interfaces;

namespace TMS.Reports.API.Controllers
{
    [Route("v1/reports")]
    [ApiController]
    public class TaskReportsController : ControllerBase
    {
        private readonly IReportAppService _reportAppService;

        public TaskReportsController(IReportAppService reportAppService)
        {
            _reportAppService = reportAppService;
        }

        [HttpGet]
        [Route("completed-tasks")]
        public IActionResult GetCompletedTasks()
        {
            return _reportAppService.GetCompletedTasks();
        }
        
        [HttpGet]
        [Route("in-progress-tasks")]
        public IActionResult GetInProgressTasksByDate(DateTime? forDate)
        {
            return _reportAppService.GetInProgressTasksByDate(forDate);
        }

    }
}
