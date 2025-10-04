using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using MediatR;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTaskComment;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.Enum;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Application.Events.Domain.TaskUpdated;
using ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class UpdateTaskCommentHandlerTests
{
    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldUpdateCommentAndPublishEventAndCommit_WhenCommentExists()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var taskId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var existingComment = new Entities.TaskComment(commentId);
        existingComment.SetData(taskId, userId, "Comentário original");

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<ITaskCommentRepository>();
        var mockMediator = new Mock<IMediator>();

        mockRepository.Setup(r => r.Get(commentId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(existingComment);

        var handler = new UpdateTaskCommentHandler(mockUnitOfWork.Object, mockMediator.Object, mockRepository.Object);

        var request = new UpdateTaskCommentRequest
        {
            Comment = "Comentário atualizado",
        };

        request.UpdateId(commentId);
        request.UpdateUserId(userId);
        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.Update(existingComment), Times.Once);
        mockMediator.Verify(m => m.Publish(It.Is<TaskUpdatedDomainEvent>(e =>
            e.UserId == userId &&
            e.Type == TaskAction.EditComment
        ), It.IsAny<CancellationToken>()), Times.Once);
        mockUnitOfWork.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(response);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldThrowInvalidOperationException_WhenCommentNotFound()
    {
        // Arrange
        var commentId = Guid.NewGuid();

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<ITaskCommentRepository>();
        var mockMediator = new Mock<IMediator>();

        mockRepository.Setup(r => r.Get(commentId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync((Entities.TaskComment)null);

        var handler = new UpdateTaskCommentHandler(mockUnitOfWork.Object, mockMediator.Object, mockRepository.Object);

        var request = new UpdateTaskCommentRequest
        {
            Comment = "Novo comentário",
        };

        request.UpdateId(commentId);
        request.UpdateUserId(Guid.NewGuid());
        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Equal($"TaskComment não encontrado. Id: {commentId}", ex.Message);
    }
}