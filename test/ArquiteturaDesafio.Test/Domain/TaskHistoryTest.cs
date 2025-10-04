using System;
using System.Collections.Generic;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using Enums = ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.ValueObjects;
using NSubstitute;
using Xunit;
using ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.Enum;
namespace ArquiteturaDesafio.Test.Domain
{
    public class TaskHistoryTest
    {
        [Fact]
        public void Constructor_ShouldSetIdCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var history = new TaskHistory(id);

            // Assert
            Assert.Equal(id, history.Id);
        }

        [Fact]
        public void SetHistoryInfo_ShouldAssignAllFieldsCorrectly()
        {
            // Arrange
            var history = new TaskHistory(Guid.NewGuid());
            var taskId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var beforeValue = "Valor anterior";
            var afterValue = "Valor novo";
            var actionType = TaskAction.UpdateTask;

            // Act
            history.SetHistoryInfo(taskId, userId, beforeValue, afterValue, actionType);

            // Assert
            Assert.Equal(taskId, history.TaskId);
            Assert.Equal(userId, history.UserId);
            Assert.Equal(beforeValue, history.BeforeValue);
            Assert.Equal(afterValue, history.AfterValue);
            Assert.Equal(actionType, history.Type);
        }


    }
}
