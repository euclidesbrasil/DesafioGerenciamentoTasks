using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.DeleteTaskComment;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class DeleteTaskCommentHandlerTests
{
    [Fact]
    public async Task Handle_ShouldDeleteCommentAndCommit_WhenCommentExists()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var comment = new Entities.TaskComment(commentId);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<ITaskCommentRepository>();

        mockRepository.Setup(r => r.Get(commentId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(comment);

        var handler = new DeleteTaskHandler(mockUnitOfWork.Object, mockRepository.Object);

        var request = new DeleteTaskCommentRequest(commentId);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.Delete(comment), Times.Once);
        mockUnitOfWork.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task Handle_ShouldThrowKeyNotFoundException_WhenCommentNotFound()
    {
        // Arrange
        var commentId = Guid.NewGuid();

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockRepository = new Mock<ITaskCommentRepository>();

        mockRepository.Setup(r => r.Get(commentId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync((Entities.TaskComment)null);

        var handler = new DeleteTaskHandler(mockUnitOfWork.Object, mockRepository.Object);

        var request = new DeleteTaskCommentRequest (commentId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Equal($"TaskComment não encontrado. Id: {commentId}", ex.Message);
    }
}