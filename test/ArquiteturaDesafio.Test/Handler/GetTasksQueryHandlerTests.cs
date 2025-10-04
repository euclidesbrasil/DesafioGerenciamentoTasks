using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterQuery;
using ArquiteturaDesafio.Core.Application.UseCases.DTOs;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.Common;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Domain.ValueObjects;
using Bogus;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.AuthenticateUser;
using NSubstitute;
using System.Reflection.Metadata;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class GetTasksQueryHandlerTests
{
    private readonly Faker _faker;
    private readonly IJwtTokenService _tokenService;

    public GetTasksQueryHandlerTests()
    {
        _faker = new Faker("pt_BR");
        _tokenService = Substitute.For<IJwtTokenService>();
    }
    [Fact]
    public async Task Handle_ShouldReturnPaginatedTasksMappedToDTOs()
    {
        // Arrange
        var mockRepository = new Mock<ITaskRepository>();
        var mockMapper = new Mock<IMapper>();

        var task1 = new Entities.Task(ArquiteturaDesafio.Core.Domain.Enum.TaskStatus.Done);
        var task2 = new Entities.Task(ArquiteturaDesafio.Core.Domain.Enum.TaskStatus.Pending);

        var paginatedResult = new PaginatedResult<Entities.Task>
        {
            Data = new List<Entities.Task> { task1, task2 },
            CurrentPage = 1,
            TotalItems = 2,
        };

        mockRepository.Setup(r => r.GetTasksPagination(It.IsAny<PaginationQuery>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(paginatedResult);

        // Simula o método de extensão MapToResponse
        var dto1 = new TaskBaseDTO { Title = "Tarefa 1" };
        var dto2 = new TaskBaseDTO { Title = "Tarefa 2" };
        var user1 = new Entities.User(_faker.Internet.Email(), "Username1", "Password", _faker.Name.FullName(), _faker.Address.Country(),
        new ArquiteturaDesafio.Core.Domain.ValueObjects.Address(_faker.Address.City(), _faker.Address.StreetName(), _faker.Random.Number(1, 1000), _faker.Address.ZipCode(), new Geolocation(_faker.Address.Latitude().ToString(), _faker.Address.Longitude().ToString())),
        _faker.Random.Replace("###########"), UserStatus.Active, UserRole.Admin, _tokenService
        );
        var user2 = new Entities.User(_faker.Internet.Email(), "Username2", "Password", _faker.Name.FullName(), _faker.Address.Country(),
                new ArquiteturaDesafio.Core.Domain.ValueObjects.Address(_faker.Address.City(), _faker.Address.StreetName(), _faker.Random.Number(1, 1000), _faker.Address.ZipCode(), new Geolocation(_faker.Address.Latitude().ToString(), _faker.Address.Longitude().ToString())),
                _faker.Random.Replace("###########"), UserStatus.Active, UserRole.Admin, _tokenService
            );
        task1.SetResponsableUser(user1);
        task2.SetResponsableUser(user2);
        mockMapper.Setup(m => m.Map<TaskBaseDTO>(task1)).Returns(dto1);
        mockMapper.Setup(m => m.Map<TaskBaseDTO>(task2)).Returns(dto2);
        var handler = new GetTasksQueryHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetTasksQueryRequest(1,10,"id asc", new Dictionary<string, string>())
        {
            page = 1,
            size = 10,
            order = "asc"
        };

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