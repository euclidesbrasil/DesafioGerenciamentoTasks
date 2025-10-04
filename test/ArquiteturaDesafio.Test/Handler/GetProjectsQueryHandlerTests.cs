using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.Project.FilterQuery;
using ArquiteturaDesafio.Core.Application.UseCases.DTOs;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.Common;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class GetProjectsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnPaginatedProjectsMappedToDTOs()
    {
        // Arrange
        var mockRepository = new Mock<IProjectRepository>();
        var mockMapper = new Mock<IMapper>();

        var project1 = new Entities.Project(Guid.NewGuid());
        var project2 = new Entities.Project(Guid.NewGuid());

        var paginatedResult = new PaginatedResult<Entities.Project>
        {
            Data = new List<Entities.Project> { project1, project2 },
            CurrentPage = 1,
            TotalItems = 2,
        };

        mockRepository.Setup(r => r.GetProjectsPagination(It.IsAny<PaginationQuery>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(paginatedResult);

        var dto1 = new ProjectBaseDTO { Id = project1.Id, Title = "Projeto 1" };
        var dto2 = new ProjectBaseDTO { Id = project2.Id, Title = "Projeto 2" };

        mockMapper.Setup(m => m.Map<ProjectBaseDTO>(project1)).Returns(dto1);
        mockMapper.Setup(m => m.Map<ProjectBaseDTO>(project2)).Returns(dto2);

        var handler = new GetProjectsQueryHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetProjectsQueryRequest(

            page: 1,
            size: 10,
            order: "id asc",
            filters: new Dictionary<string, string>()
        );

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(2, response.Data.Count);
        Assert.Equal(1, response.CurrentPage);
        Assert.Equal(2, response.TotalItems);
        Assert.Equal(1, response.TotalPages);
    }
}