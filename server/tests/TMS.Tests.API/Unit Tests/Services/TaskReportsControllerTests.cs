using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.IO;
using System.Text;
using TMS.Application.Interfaces;
using TMS.Reports.API.Controllers;
using Xunit;

namespace TMS.Tests.API.Unit_Tests.Services
{
    public class TaskReportsControllerTests
    {
        public TaskReportsController taskReportsController { get; set; }
        public Mock<IReportAppService> reportAppService { get; set; }

        public TaskReportsControllerTests()
        {
            reportAppService = new Mock<IReportAppService>();

            taskReportsController = new TaskReportsController(reportAppService.Object);
        }

        // AAA => Arrange, Act, Assert
        [Fact(DisplayName = "Get all completed task")]
        [Trait("Reports", "Task Reports Controller")]
        public void TaskReportsController_GetCompletedTasks_ReturnWithSuccess()
        {
            // Arrange
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("Name,Description,Start Date,Finish Date,State\r\n"));
            var file = new FileStreamResult(stream, "text/plain")
            {
                FileDownloadName = "Report_Completed_" + DateTime.Now + ".csv"
            };
            reportAppService.Setup(m => m.GetCompletedTasks()).Returns(file);

            // Act
            var result = taskReportsController.GetCompletedTasks();

            // Assert
            reportAppService.Verify(m => m.GetCompletedTasks(), Times.Once);
            Assert.IsType<FileStreamResult>(result);
        }
    }
}
