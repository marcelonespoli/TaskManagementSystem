using System.Collections.Generic;
using TMS.Application.ViewModels;

namespace TMS.Application.Interfaces
{
    public interface IReportAppService
    {
        IEnumerable<TaskViewModel> GetCompletedTasks();
    }
}
