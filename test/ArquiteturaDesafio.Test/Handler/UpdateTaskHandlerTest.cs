using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using MediatR;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTask;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using Enums = ArquiteturaDesafio.Core.Domain.Enum;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Application.Events.Domain.TaskUpdated;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Project.UpdateProject;
using ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class UpdateTaskHandlerTest
{
    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldUpdateTaskAndPublishEventAndCommit_WhenValid()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var existingTask = new Entities.Task(Enums.TaskStatus.Pending);
        existingTask.SetPriority(Enums.TaskPriority.Medium);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<ITaskRepository>();
        var mockMediator = new Mock<IMediator>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(r => r.Get(taskId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(existingTask);

        mockRepository.Setup(r => r.LimitHasReachedByProject(projectId, taskId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(false);

        var entity = new Entities.Task(Enums.TaskStatus.Pending);
        entity.SetPriority(Enums.TaskPriority.Medium);
        mockMapper.Setup(m => m.Map<Entities.Task>(It.IsAny<Entities.Task>()))
                  .Returns(entity);

        var handler = new UpdateTaskHandler(mockUnitOfWork.Object, mockRepository.Object, mockMediator.Object, mockMapper.Object);

        var request = new UpdateTaskRequest();
        request.PopulateRequest(taskId,userId,"Nova tarefa", "Descrição atualizada", projectId, DateTime.Today.AddDays(5), Enums.TaskStatus.Pending);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.Update(existingTask), Times.Once);
        mockMediator.Verify(m => m.Publish(It.IsAny<TaskUpdatedDomainEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        mockUnitOfWork.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(response);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldThrowInvalidOperationException_WhenTaskNotFound()
    {
        // Arrange
        var mockRepository = new Mock<ITaskRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockMediator = new Mock<IMediator>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(r => r.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync((Entities.Task)null);

        var handler = new UpdateTaskHandler(mockUnitOfWork.Object, mockRepository.Object, mockMediator.Object, mockMapper.Object);

        var request = new UpdateTaskRequest();
        request.UpdateId(Guid.NewGuid());
        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Contains("Task não encontrado", ex.Message);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldThrowArgumentException_WhenLimitExceeded()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var projectId = Guid.NewGuid();

        var existingTask = new Entities.Task(Enums.TaskStatus.Pending);
        existingTask.SetPriority(Enums.TaskPriority.Medium);
        existingTask.SetProjectId(projectId);
        existingTask.Id = taskId;

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<ITaskRepository>();
        var mockMediator = new Mock<IMediator>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(r => r.Get(taskId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(existingTask);

        mockRepository.Setup(r => r.LimitHasReachedByProject(projectId, taskId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(true);
        var entity = new Entities.Task(Enums.TaskStatus.Pending);
        entity.SetPriority(Enums.TaskPriority.Medium);
        mockMapper.Setup(m => m.Map<Entities.Task>(It.IsAny<Entities.Task>()))
                  .Returns(entity);

        var handler = new UpdateTaskHandler(mockUnitOfWork.Object, mockRepository.Object, mockMediator.Object, mockMapper.Object);

        var request = new UpdateTaskRequest();
        request.PopulateRequest(taskId, entity.ResponsableUserId, "Teste", "Teste", projectId, DateTime.Today, Enums.TaskStatus.Pending);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Equal("Não é possível gravar a tarefa. Cada projeto tem um limite máximo de 20 tarefas.", ex.Message);
    }
}
