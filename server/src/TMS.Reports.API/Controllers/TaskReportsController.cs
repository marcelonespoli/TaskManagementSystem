using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TMS.Application.Interfaces;
using TMS.Application.ViewModels;

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
        public IEnumerable<TaskViewModel> GetCompleteTasks()
        {
            return _reportAppService.GetCompleteTasks();
        }

    }
}
