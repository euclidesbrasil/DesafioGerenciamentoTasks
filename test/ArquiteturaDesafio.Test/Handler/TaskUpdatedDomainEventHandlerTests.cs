using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Newtonsoft.Json;
using ArquiteturaDesafio.Core.Application.Events.Domain.TaskUpdated;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class TaskUpdatedDomainEventHandlerTests
{
    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldCreateTaskHistory_WhenNotificationIsValid()
    {
        // Arrange
        var mockHistoryRepo = new Mock<ITaskHistoryRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        var before = new Entities.Task(Core.Domain.Enum.TaskStatus.Pending);
        var after = new Entities.Task(Core.Domain.Enum.TaskStatus.Doing);

        var notification = new TaskUpdatedDomainEvent(
            after, before, Guid.NewGuid(), Guid.NewGuid(), TaskAction.AddComment);

        var handler = new TaskUpdatedDomainEventHandler(mockHistoryRepo.Object, mockUnitOfWork.Object);

        // Act
        await handler.Handle(notification, CancellationToken.None);

        // Assert
        mockHistoryRepo.Verify(r => r.Create(It.Is<TaskHistory>(h =>
            h.UserId == notification.UserId &&
            h.Type == notification.Type
        )), Times.Once);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldUseProvidedJson_WhenJsonBeforeAndAfterAreNotEmpty()
    {
        // Arrange
        var mockHistoryRepo = new Mock<ITaskHistoryRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var notification = new TaskUpdatedDomainEvent("string after", "string before", Guid.NewGuid(), Guid.NewGuid(), TaskAction.EditComment);

        var handler = new TaskUpdatedDomainEventHandler(mockHistoryRepo.Object, mockUnitOfWork.Object);

        // Act
        await handler.Handle(notification, CancellationToken.None);

        // Assert
        mockHistoryRepo.Verify(r => r.Create(It.Is<TaskHistory>(h =>
             h.UserId == notification.UserId &&
             h.Type == notification.Type
         )), Times.Once);
    }
}