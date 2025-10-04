using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.TaskComment.CreateTaskComment;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Application.Events.Domain.TaskUpdated;
using MediatR;
using System.Timers;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class CreateTaskCommentHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateCommentAndPublishEventAndCommit()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<ITaskCommentRepository>();
        var mockMediator = new Mock<IMediator>();

        var handler = new CreateTaskCommentHandler(mockUnitOfWork.Object, mockMediator.Object, mockRepository.Object);

        var request = new CreateTaskCommentRequest();
        request.SetTaskId(Guid.NewGuid());
        request.UpdateUserId(Guid.NewGuid());
        request.Comment = new string('A', 100); // Comentário com 100 caracteres

        var cancellationToken = CancellationToken.None;

        // Act
        var response = await handler.Handle(request, cancellationToken);

        // Assert
        mockRepository.Verify(r => r.Create(It.IsAny<ArquiteturaDesafio.Core.Domain.Entities.TaskComment>()), Times.Once);
        mockMediator.Verify(m => m.Publish(It.Is<TaskUpdatedDomainEvent>(e =>
            e.UserId == request.UserId &&
            e.Type == TaskAction.AddComment
        ), cancellationToken), Times.Once);
        mockUnitOfWork.Verify(u => u.Commit(cancellationToken), Times.Once);

        Assert.NotEqual(Guid.Empty, response.id);
    }
}