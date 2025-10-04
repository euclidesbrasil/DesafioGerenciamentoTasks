using System;
using Xunit;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTask;

namespace ArquiteturaDesafio.Tests.Application.Handlers.Validator;
public class UpdateTaskValidatorTests
{
    private readonly UpdateTaskValidator _validator = new UpdateTaskValidator();

    [Fact]
    public void Should_PassValidation_WhenRequestIsValid()
    {
        var request = new UpdateTaskRequest
        {
            Title = "Título válido",
            Description = "Descrição adequada",
            Status = Core.Domain.Enum.TaskStatus.Done
        };
        request.UpdateId(Guid.NewGuid());
        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_FailValidation_WhenIdIsEmpty()
    {
        var request = new UpdateTaskRequest
        {
            Title = "Título válido",
            Description = "Descrição válida"
        };
        request.UpdateId(Guid.Empty);

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Id");
    }

    [Fact]
    public void Should_FailValidation_WhenTitleIsEmpty()
    {
        var request = new UpdateTaskRequest
        {
            Title = "",
            Description = "Descrição válida"
        };
        request.UpdateId(Guid.NewGuid());

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }

    [Fact]
    public void Should_FailValidation_WhenDescriptionIsTooLong()
    {
        var request = new UpdateTaskRequest
        {
            Title = "Título válido",
            Description = new string('x', 1001)
        };
        request.UpdateId(Guid.NewGuid());

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description");
    }
}