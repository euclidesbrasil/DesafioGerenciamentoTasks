using System;
using System.Collections.Generic;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using Enums = ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.ValueObjects;
using NSubstitute;
using Xunit;
using ArquiteturaDesafio.Core.Domain.Enum;
namespace ArquiteturaDesafio.Test.Domain
{
    public class TaskTests
    {
        [Fact]
        public void Constructor_ShouldSetStatusAndGenerateId()
        {
            // Arrange
            var status = Enums.TaskStatus.Done;

            // Act
            var task = new Entities.Task(status);

            // Assert
            Assert.Equal(status, task.Status);
            Assert.NotEqual(Guid.Empty, task.Id);
        }

        [Fact]
        public void SetNewTask_ShouldGenerateNewIdAndSetStatusToPending()
        {
            // Arrange
            var task = new Entities.Task(Enums.TaskStatus.Done);

            // Act
            task.setNewTask();

            // Assert
            Assert.NotEqual(Guid.Empty, task.Id);
            Assert.Equal(Enums.TaskStatus.Pending, task.Status);
        }

        [Fact]
        public void UpdateTaskInfo_ShouldUpdateAllFields()
        {
            // Arrange
            var task = new Entities.Task(Enums.TaskStatus.Pending);
            var newTitle = "Nova tarefa";
            var newDesc = "Descrição atualizada";
            var newDate = DateTime.Today.AddDays(7);
            var newStatus = Enums.TaskStatus.Done;

            // Act
            task.UpdateTaskInfo(newTitle, newDesc, newDate, newStatus);

            // Assert
            Assert.Equal(newTitle, task.Title);
            Assert.Equal(newDesc, task.Description);
            Assert.Equal(newDate, task.DateExpiration);
            Assert.Equal(newStatus, task.Status);
        }

        [Fact]
        public void AssociateToProject_ShouldSetProjectId()
        {
            // Arrange
            var task = new Entities.Task(Enums.TaskStatus.Pending);
            var projectId = Guid.NewGuid();

            // Act
            task.AssociateToProject(projectId);

            // Assert
            Assert.Equal(projectId, task.ProjectId);
        }

        [Fact]
        public void ValidadeTaskRules_ShouldThrowIfPriorityChanged()
        {
            // Arrange
            var beforeTask = new Entities.Task(Enums.TaskStatus.Pending);
            beforeTask.UpdateTaskInfo("Tarefa", "Descrição", DateTime.Today, Enums.TaskStatus.Pending);
            beforeTask.SetPriority(TaskPriority.Low);

            var currentTask = new Entities.Task(Enums.TaskStatus.Pending);
            currentTask.UpdateTaskInfo("Tarefa", "Descrição", DateTime.Today, Enums.TaskStatus.Pending);
            currentTask.SetPriority(TaskPriority.Hight);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => currentTask.ValidadeTaskRules(beforeTask));
            Assert.Equal("The priority can't be changed.", ex.Message);
        }

        [Fact]
        public void ValidadeTaskRules_ShouldNotThrowIfPriorityIsSame()
        {
            // Arrange
            var beforeTask = new Entities.Task(Enums.TaskStatus.Pending);
            beforeTask.SetPriority(TaskPriority.Medium);

            var currentTask = new Entities.Task(Enums.TaskStatus.Pending);
            currentTask.SetPriority(TaskPriority.Medium);

            // Act & Assert
            currentTask.ValidadeTaskRules(beforeTask); // Não deve lançar exceção
        }

    }
}
