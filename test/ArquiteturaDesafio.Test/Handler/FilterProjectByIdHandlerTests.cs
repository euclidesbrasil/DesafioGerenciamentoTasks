using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.Project.FilterById;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterById;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class FilterProjectByIdHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedResponse_WhenProjectExists()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = new Entities.Project(projectId);

        var expectedResponse = new FilterProjectByIdResponse
        {
            Id = projectId,
            Title = "Projeto Teste",
            Description = "Descrição do projeto"
        };

        var mockRepository = new Mock<IProjectRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(r => r.Get(projectId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(project);

        // Simula o método de extensão MapToResponse
        mockMapper.Setup(m => m.Map<FilterProjectByIdResponse>(project))
                  .Returns(expectedResponse);

        var handler = new FilterProjectByIdHandler(mockRepository.Object, mockMapper.Object);
        var request = new FilterProjectByIdRequest(projectId);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(expectedResponse.Id, response.Id);
    }

    [Fact]
    public async Task Handle_ShouldThrowInvalidOperationException_WhenProjectNotFound()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        var mockRepository = new Mock<IProjectRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(r => r.Get(projectId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync((Entities.Project)null);

        var handler = new FilterProjectByIdHandler(mockRepository.Object, mockMapper.Object);
        var request = new FilterProjectByIdRequest(projectId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Equal($"Project não encontrado. Id: {projectId}", ex.Message);
    }
}