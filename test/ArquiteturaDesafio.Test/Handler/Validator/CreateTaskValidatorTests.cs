using System;
using Xunit;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.CreateTask;
using ArquiteturaDesafio.Core.Domain.Enum;

namespace ArquiteturaDesafio.Tests.Application.Handlers.Validator;
public class CreateTaskValidatorTests
{
    private readonly CreateTaskValidator _validator = new CreateTaskValidator();

    [Fact]
    public void Should_PassValidation_WhenRequestIsValid()
    {
        var request = new CreateTaskRequest
        {
            Title = "Tarefa válida",
            Description = "Descrição adequada",
            DateExpiration = DateTime.Now.AddDays(1),
            ResponsableUserId = Guid.NewGuid(),
            Priority = TaskPriority.Medium
        };

        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_Fail_WhenTitleIsEmpty()
    {
        var request = new CreateTaskRequest
        {
            Title = "",
            Description = "Descrição válida",
            DateExpiration = DateTime.Now.AddDays(1),
            ResponsableUserId = Guid.NewGuid(),
            Priority = TaskPriority.Low
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }

    [Fact]
    public void Should_Fail_WhenDescriptionIsTooLong()
    {
        var request = new CreateTaskRequest
        {
            Title = "Título válido",
            Description = new string('x', 1001),
            DateExpiration = DateTime.Now.AddDays(1),
            ResponsableUserId = Guid.NewGuid(),
            Priority = TaskPriority.Low
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description");
    }

    [Fact]
    public void Should_Fail_WhenDateExpirationIsInPast()
    {
        var request = new CreateTaskRequest
        {
            Title = "Título válido",
            Description = "Descrição válida",
            DateExpiration = DateTime.Now.AddMinutes(-1),
            ResponsableUserId = Guid.NewGuid(),
            Priority = TaskPriority.Low
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "DateExpiration");
    }

    [Fact]
    public void Should_Fail_WhenPriorityIsNone()
    {
        var request = new CreateTaskRequest
        {
            Title = "Título válido",
            Description = "Descrição válida",
            DateExpiration = DateTime.Now.AddDays(1),
            ResponsableUserId = Guid.NewGuid(),
            Priority = TaskPriority.None
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Priority");
    }
}