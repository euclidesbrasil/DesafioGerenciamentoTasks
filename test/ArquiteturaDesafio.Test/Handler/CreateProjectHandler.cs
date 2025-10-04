using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.User.CreateProject;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class CreateProjectHandlerTest
{
    [Fact]
    public async Task Handle_ShouldCreateProjectAndCommit()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<IProjectRepository>();
        var mockMapper = new Mock<IMapper>();

        var projectId = Guid.NewGuid();
        var projectEntity = new Entities.Project(projectId);

        mockMapper.Setup(m => m.Map<Entities.Project>(It.IsAny<CreateProjectRequest>()))
                  .Returns(projectEntity);

        var handler = new CreateProjectHandler(mockUnitOfWork.Object, mockRepository.Object, mockMapper.Object);

        var request = new CreateProjectRequest("Projeto Teste", "Descrição do projeto", Guid.NewGuid());

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.Create(projectEntity), Times.Once);
        mockUnitOfWork.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(projectId, response.id);
    }
}