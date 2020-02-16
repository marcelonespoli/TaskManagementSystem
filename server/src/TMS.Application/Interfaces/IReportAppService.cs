using Microsoft.AspNetCore.Mvc;
using System;

namespace TMS.Application.Interfaces
{
    public interface IReportAppService
    {
        FileStreamResult GetCompletedTasks();
        FileStreamResult GetInProgressTasksByDate(DateTime? date);
    }
}
