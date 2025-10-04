using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.CreateTask;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using Enums = ArquiteturaDesafio.Core.Domain.Enum;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class CreateTaskHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateTaskAndCommit_WhenLimitNotReached()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<ITaskRepository>();
        var mockMapper = new Mock<IMapper>();

        var projectId = Guid.NewGuid();

        var taskEntity = new Entities.Task(Enums.TaskStatus.Pending);
        taskEntity.SetProjectId(projectId);
        taskEntity.setNewTask();

        mockMapper.Setup(m => m.Map<Entities.Task>(It.IsAny<CreateTaskRequest>()))
                  .Returns(taskEntity);

        mockRepository.Setup(r => r.LimitHasReachedByProject(projectId, taskEntity.Id, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(false);

        var handler = new CreateTaskHandler(mockUnitOfWork.Object, mockRepository.Object, mockMapper.Object);

        var request = new CreateTaskRequest
        {
            Title = "Nova tarefa",
            Description = "Descrição",
            DateExpiration = DateTime.Today.AddDays(5),
            Priority = Enums.TaskPriority.Medium,
            ResponsableUserId = Guid.NewGuid()
        };

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.Create(taskEntity), Times.Once);
        mockUnitOfWork.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(response.id);
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentException_WhenLimitReached()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<ITaskRepository>();
        var mockMapper = new Mock<IMapper>();

        var projectId = Guid.NewGuid();

        var taskEntity = new Entities.Task(Enums.TaskStatus.Pending);
        taskEntity.SetProjectId(projectId);
        taskEntity.setNewTask();

        mockMapper.Setup(m => m.Map<Entities.Task>(It.IsAny<CreateTaskRequest>()))
                  .Returns(taskEntity);

        mockRepository.Setup(r => r.LimitHasReachedByProject(projectId, It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(true);

        var handler = new CreateTaskHandler(mockUnitOfWork.Object, mockRepository.Object, mockMapper.Object);

        var request = new CreateTaskRequest
        {
            Title = "Tarefa",
            Description = "Descrição",
            DateExpiration = DateTime.Today,
            Priority = Enums.TaskPriority.Low,
            ResponsableUserId = Guid.NewGuid(),
            ProjectId = projectId
        };
        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Equal("Não é possível gravar a tarefa. Cada projeto tem um limite máximo de 20 tarefas.", ex.Message);
    }
}