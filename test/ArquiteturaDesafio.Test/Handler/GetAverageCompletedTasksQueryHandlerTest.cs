using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.GetAverageCompletedTasksQuery;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class GetAverageCompletedTasksQueryHandlerTest
{
    [Fact]
    public async Task Handle_ShouldReturnAverageAndDetails_WhenTasksExist()
    {
        Guid user1 = Guid.NewGuid();
        Guid user2 = Guid.NewGuid();
        // Arrange
        var t1 = new Entities.Task(ArquiteturaDesafio.Core.Domain.Enum.TaskStatus.Done); 
        t1.setNewTask();
        t1.SetResponsableUserId(user1);
        t1.SetResponsableUser(new Entities.User(t1.Id));
        var t2 = new Entities.Task(ArquiteturaDesafio.Core.Domain.Enum.TaskStatus.Done); 
        t2.setNewTask();
        t2.SetResponsableUserId(user1);
        t2.SetResponsableUser(new Entities.User(t2.Id));
        var t3 = new Entities.Task(ArquiteturaDesafio.Core.Domain.Enum.TaskStatus.Done); 
        t3.setNewTask();
        t3.SetResponsableUserId(user2);
        t3.SetResponsableUser(new Entities.User(t3.Id));

        var tasks = new List<Entities.Task> {t1,t2,t3};

        var mockRepository = new Mock<ITaskRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(r => r.ListAllDoneTaskIn30Days(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(tasks);

        var handler = new GetUsersByIdHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetAverageCompletedTasksQueryRequest();

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(3, response.Details.Sum(d => d.TotalCompleted));
        Assert.Equal(1.5M, response.Avarage); // 3 tasks / 2 users
        Assert.Equal(2, response.Details.Count());
    }

    [Fact]
    public async Task Handle_ShouldReturnZeroAverage_WhenNoTasksExist()
    {
        // Arrange
        var mockRepository = new Mock<ITaskRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(r => r.ListAllDoneTaskIn30Days(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new List<Entities.Task>());

        var handler = new GetUsersByIdHandler(mockRepository.Object, mockMapper.Object);

        var request = new GetAverageCompletedTasksQueryRequest();

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(0.0M, response.Avarage);
        Assert.Empty(response.Details);
    }
}