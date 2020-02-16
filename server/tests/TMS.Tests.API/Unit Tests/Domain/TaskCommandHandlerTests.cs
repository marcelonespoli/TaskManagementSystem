using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMS.Domain;
using TMS.Domain.Core.Notifications;
using TMS.Domain.Interfaces;
using TMS.Domain.Tasks.Commands;
using TMS.Domain.Tasks.Events;
using TMS.Domain.Tasks.Repository;
using Xunit;

namespace TMS.Tests.API.Unit_Tests.Domain
{
    public class TaskCommandHandlerTests
    {
        public TaskCommandHandler taskCommandHandler { get; set; }
        public CreateTaskCommand createTaskCommand { get; set; }
        public Mock<IMapper> mockMapper { get; set; }
        public Mock<IUnitOfWork> mockUnitOfWork { get; set; }
        public Mock<IMediatorHandler> mockMediatorHandler { get; set; }
        public Mock<ITaskRepository> mockTaskRepository { get; set; }
        public Mock<DomainNotificationHandler> mockNotifications { get; set; }

        public TaskCommandHandlerTests()
        {
            mockMapper = new Mock<IMapper>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockMediatorHandler = new Mock<IMediatorHandler>();
            mockTaskRepository = new Mock<ITaskRepository>();
            mockNotifications = new Mock<DomainNotificationHandler>();

            createTaskCommand = new CreateTaskCommand(new Guid("4C242A94-97E1-4055-9772-26860D1CDC5B"), "Task1 test","", null, null, new List<AddSubtaskCommand>());            

            taskCommandHandler = new TaskCommandHandler(mockUnitOfWork.Object, mockMediatorHandler.Object,
                mockTaskRepository.Object, mockNotifications.Object);
        }

        // AAA => Arrange, Act, Assert
        [Fact(DisplayName = "Create task with success")]
        [Trait("Domain", "Task Command Handler")]
        public async Task TaskCommandHandler_HandleCreateTaskCommand_ReturnWithSuccess()
        {
            // Arrange
            mockUnitOfWork.Setup(m => m.Commit()).Returns(true);

            // Act
            var result = await taskCommandHandler.Handle(createTaskCommand, CancellationToken.None);

            // Assert
            mockTaskRepository.Verify(m => m.Add(It.IsAny<TaskData>()), Times.Once);
            mockMediatorHandler.Verify(m => m.PublishEvent(It.IsAny<TaskCreatedEvent>()), Times.Once);
            Assert.True(result);
        }

        [Fact(DisplayName = "Create task with NOT success")]
        [Trait("Domain", "Task Command Handler")]
        public async Task TaskCommandHandler_HandleCreateTaskCommand_ReturnWithNOTSuccess()
        {
            // Arrange
            createTaskCommand = new CreateTaskCommand(new Guid("4C242A94-97E1-4055-9772-26860D1CDC5B"), 
                "", "Description Task1 test", DateTime.Now.AddDays(2), DateTime.Now.AddDays(1), new List<AddSubtaskCommand>());

            // Act
            var result = await taskCommandHandler.Handle(createTaskCommand, CancellationToken.None);

            // Assert
            mockUnitOfWork.Verify(m => m.Commit(), Times.Never);
            mockTaskRepository.Verify(m => m.Add(It.IsAny<TaskData>()), Times.Never);           
            mockMediatorHandler.Verify(m => m.PublishEvent(It.IsAny<TaskCreatedEvent>()), Times.Never);
            Assert.False(result);
        }

        [Fact(DisplayName = "Create task commit failed")]
        [Trait("Domain", "Task Command Handler")]
        public async Task TaskCommandHandler_HandleCreateTaskCommand_ReturnCommitFailed()
        {
            // Arrange
            mockUnitOfWork.Setup(m => m.Commit()).Returns(false);

            // Act
            var result = await taskCommandHandler.Handle(createTaskCommand, CancellationToken.None);

            // Assert
            mockTaskRepository.Verify(m => m.Add(It.IsAny<TaskData>()), Times.Once);
            mockMediatorHandler.Verify(m => m.PublishEvent(It.IsAny<TaskCreatedEvent>()), Times.Never);
            Assert.True(result);
        }

    }
}
