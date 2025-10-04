using System;
using System.Collections.Generic;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using Enums = ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.ValueObjects;
using NSubstitute;
using Xunit;
namespace ArquiteturaDesafio.Test.Domain
{
    public class ProjectTest
    {
        [Fact]
        public void UpdateInfo_ShouldUpdateTitleAndDescription()
        {
            // Arrange
            var project = new Entities.Project(Guid.NewGuid());
            var newTitle = "Novo Título";
            var newDescription = "Nova descrição do projeto";

            // Act
            project.UpdateInfo(newTitle, newDescription);

            // Assert
            Assert.Equal(newTitle, project.Title);
            Assert.Equal(newDescription, project.Description);
        }

        [Fact]
        public void SetUser_ShouldAssignUserId()
        {
            // Arrange
            var project = new Entities.Project(Guid.NewGuid());
            var userId = Guid.NewGuid();

            // Act
            project.SetUser(userId);

            // Assert
            Assert.Equal(userId, project.UserId);
        }

        [Fact]
        public void AddTasks_ShouldAddTasksToProject()
        {
            // Arrange
            var project = new Entities.Project(Guid.NewGuid());
            var tasks = new List<Entities.Task>
            {
                new Entities.Task(Enums.TaskStatus.Pending),
                new Entities.Task(Enums.TaskStatus.Pending)
            };

            // Act
            project.AddTasks(tasks);

            // Assert
            Assert.NotNull(project.Tasks);
            Assert.Equal(2, project.Tasks.Count);
        }
    }
}
