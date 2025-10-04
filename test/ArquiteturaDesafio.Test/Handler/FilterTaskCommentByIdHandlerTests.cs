using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.TaskComment.FilterById;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class FilterTaskCommentByIdHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedResponse_WhenCommentExists()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var comment = new Entities.TaskComment(commentId);
        comment.SetData(Guid.NewGuid(), Guid.NewGuid(), "Comentário de teste");

        var expectedResponse = new FilterTaskCommentByIdResponse
        {
            Id = comment.Id,
            Comment = comment.Comment
        };

        var mockRepository = new Mock<ITaskCommentRepository>();
        mockRepository.Setup(r => r.Get(commentId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(comment);

        var handler = new FilterTaskCommentByIdHandler(mockRepository.Object);
        var request = new FilterTaskCommentByIdRequest(commentId);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(expectedResponse.Id, response.Id);
        Assert.Equal(expectedResponse.Comment, response.Comment);
    }

    [Fact]
    public async Task Handle_ShouldThrowInvalidOperationException_WhenCommentNotFound()
    {
        // Arrange
        var commentId = Guid.NewGuid();

        var mockRepository = new Mock<ITaskCommentRepository>();
        mockRepository.Setup(r => r.Get(commentId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync((Entities.TaskComment)null);

        var handler = new FilterTaskCommentByIdHandler(mockRepository.Object);
        var request = new FilterTaskCommentByIdRequest(commentId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Equal($"TaskComment não encontrado. Id: {commentId}", ex.Message);
    }
}