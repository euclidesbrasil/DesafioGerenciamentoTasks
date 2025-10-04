using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using MediatR;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.DeleteTask;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using Enums = ArquiteturaDesafio.Core.Domain.Enum;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class DeleteTaskHandlerTests
{
    [Fact]
    public async Task Handle_ShouldDeleteTaskAndCommit_WhenTaskExists()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<ITaskRepository>();
        var mockMapper = new Mock<IMapper>();

        var existingTask = new Entities.Task(Enums.TaskStatus.Pending);
        mockRepository.Setup(r => r.Get(taskId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(existingTask);

        var handler = new DeleteTaskHandler(mockUnitOfWork.Object, mockRepository.Object, mockMapper.Object);
        var request = new DeleteTaskRequest(taskId);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.Delete(existingTask), Times.Once);
        mockUnitOfWork.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenTaskDoesNotExist()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<ITaskRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(r => r.Get(taskId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync((Entities.Task)null);

        var handler = new DeleteTaskHandler(mockUnitOfWork.Object, mockRepository.Object, mockMapper.Object);
        var request = new DeleteTaskRequest (taskId);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Equal($"Task não encontrado. Id: {taskId}", exception.Message);
    }
}