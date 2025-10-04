using System;
using System.Collections.Generic;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using Enums = ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.ValueObjects;
using NSubstitute;
using Xunit;
using ArquiteturaDesafio.Core.Domain.Entities;
namespace ArquiteturaDesafio.Test.Domain
{
    public class TaskCommentTest
    {
        [Fact]
        public void UpdateComment_ShouldUpdateCommentText()
        {
            // Arrange
            var commentId = Guid.NewGuid();
            var taskComment = new TaskComment(commentId);
            var newComment = "Comentário atualizado";

            // Act
            taskComment.UpdateComment(newComment);

            // Assert
            Assert.Equal(newComment, taskComment.Comment);
        }

        [Fact]
        public void SetData_ShouldAssignTaskIdUserIdAndComment()
        {
            // Arrange
            var taskComment = new TaskComment(Guid.NewGuid());
            var taskId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var commentText = "Novo comentário";

            // Act
            taskComment.SetData(taskId, userId, commentText);

            // Assert
            Assert.NotEqual(Guid.Empty, taskComment.Id); // Novo Id gerado
            Assert.Equal(taskId, taskComment.TaskId);
            Assert.Equal(userId, taskComment.UserId);
            Assert.Equal(commentText, taskComment.Comment);
        }

        [Fact]
        public void Constructor_ShouldSetIdCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var taskComment = new TaskComment(id);

            // Assert
            Assert.Equal(id, taskComment.Id);
        }

    }
}
