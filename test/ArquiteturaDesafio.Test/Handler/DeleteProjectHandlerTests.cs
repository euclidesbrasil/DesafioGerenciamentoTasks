using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using MediatR;
using ArquiteturaDesafio.Application.UseCases.Commands.User.DeleteProject;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class DeleteProjectHandlerTests
{
    [Fact]
    public async Task Handle_ShouldDeleteProjectAndCommit_WhenNoPendingTasks()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = new Entities.Project(projectId);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProjectRepo = new Mock<IProjectRepository>();
        var mockTaskRepo = new Mock<ITaskRepository>();
        var mockMapper = new Mock<IMapper>();

        mockProjectRepo.Setup(r => r.Get(projectId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(project);

        mockTaskRepo.Setup(r => r.HasAnyPendinigTaskByProject(projectId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false);

        var handler = new DeleteProjectHandler(mockUnitOfWork.Object, mockProjectRepo.Object, mockTaskRepo.Object, mockMapper.Object);

        var request = new DeleteProjectRequest(projectId);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        mockProjectRepo.Verify(r => r.Delete(project), Times.Once);
        mockUnitOfWork.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenProjectNotFound()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProjectRepo = new Mock<IProjectRepository>();
        var mockTaskRepo = new Mock<ITaskRepository>();
        var mockMapper = new Mock<IMapper>();

        mockProjectRepo.Setup(r => r.Get(projectId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Entities.Project)null);

        var handler = new DeleteProjectHandler(mockUnitOfWork.Object, mockProjectRepo.Object, mockTaskRepo.Object, mockMapper.Object);

        var request = new DeleteProjectRequest(projectId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Equal($"Projeto não encontrado. Id: {projectId}", ex.Message);
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentException_WhenPendingTasksExist()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = new Entities.Project(projectId);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockProjectRepo = new Mock<IProjectRepository>();
        var mockTaskRepo = new Mock<ITaskRepository>();
        var mockMapper = new Mock<IMapper>();

        mockProjectRepo.Setup(r => r.Get(projectId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(project);

        mockTaskRepo.Setup(r => r.HasAnyPendinigTaskByProject(projectId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        var handler = new DeleteProjectHandler(mockUnitOfWork.Object, mockProjectRepo.Object, mockTaskRepo.Object, mockMapper.Object);

        var request = new DeleteProjectRequest(projectId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Equal("Não é possível excluir o projeto, pois existem tarefas pendentes associadas a ele.", ex.Message);
    }
}