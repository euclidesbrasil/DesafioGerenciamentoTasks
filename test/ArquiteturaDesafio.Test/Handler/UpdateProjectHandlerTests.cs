using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Project.UpdateProject;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class UpdateProjectHandlerTests
{
    [Fact]
    public async Task Handle_ShouldUpdateProjectAndCommit_WhenProjectExists()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var existingProject = new Entities.Project(projectId);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<IProjectRepository>();

        mockRepository.Setup(r => r.Get(projectId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(existingProject);

        var handler = new UpdateProjectHandler(mockUnitOfWork.Object, mockRepository.Object, Mock.Of<IMapper>());

        var request = new UpdateProjectRequest
        {
            Title = "Projeto Atualizado",
            Description = "Descrição atualizada",
            UserId = userId
        };
        request.SetId(projectId);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.Update(existingProject), Times.Once);
        mockUnitOfWork.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task Handle_ShouldThrowInvalidOperationException_WhenProjectNotFound()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<IProjectRepository>();

        mockRepository.Setup(r => r.Get(projectId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync((Entities.Project)null);

        var handler = new UpdateProjectHandler(mockUnitOfWork.Object, mockRepository.Object, Mock.Of<IMapper>());

        var request = new UpdateProjectRequest
        {
            Title = "Projeto",
            Description = "Descrição",
            UserId = Guid.NewGuid()
        };
        request.SetId(projectId);
        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Equal($"Project não encontrado. Id: {projectId}", ex.Message);
    }
}