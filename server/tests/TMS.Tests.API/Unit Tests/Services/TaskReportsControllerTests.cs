using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TMS.Application.Interfaces;
using TMS.Application.ViewModels;
using TMS.Domain;
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
            var tasks = new List<TaskViewModel>
            {
                new TaskViewModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 1 test",
                    Description = "Task description test",
                    StartDate = DateTime.Now.AddDays(-5),
                    FinishDate = DateTime.Now.AddDays(-2),
                    State = States.Completed
                }
            };

            reportAppService.Setup(m => m.GetCompletedTasks()).Returns(tasks);

            // Act
            var result = taskReportsController.GetCompletedTasks();

            // Assert
            reportAppService.Verify(m => m.GetCompletedTasks(), Times.Once);
            Assert.True(result.Any());
        }
    }
}
