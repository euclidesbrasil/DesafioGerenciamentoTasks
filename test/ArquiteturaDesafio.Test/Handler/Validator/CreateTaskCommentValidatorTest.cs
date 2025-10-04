using System;
using Xunit;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.TaskComment.CreateTaskComment;

namespace ArquiteturaDesafio.Tests.Application.Handlers.Validator;
public class CreateTaskCommentValidatorTests
{
    private readonly CreateTaskCommentValidator _validator = new CreateTaskCommentValidator();

    [Fact]
    public void Should_PassValidation_WhenRequestIsValid()
    {
        var request = new CreateTaskCommentRequest
        {
            Comment = "Comentário válido"
        };

        request.SetTaskId(Guid.NewGuid());
        request.UpdateUserId(Guid.NewGuid());
        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_FailValidation_WhenTaskIdIsEmpty()
    {
        var request = new CreateTaskCommentRequest
        {
            Comment = "Comentário válido"
        };
        request.SetTaskId(Guid.Empty);
        request.UpdateUserId(Guid.NewGuid());

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "TaskId");
    }

    [Fact]
    public void Should_FailValidation_WhenUserIdIsEmpty()
    {
        var request = new CreateTaskCommentRequest
        {
            Comment = "Comentário válido"
        };
        request.SetTaskId(Guid.NewGuid());
        request.UpdateUserId(Guid.Empty);

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "UserId");
    }
}