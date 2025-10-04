using System;
using Xunit;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTaskComment;

namespace ArquiteturaDesafio.Tests.Application.Handlers.Validator;
public class UpdateTaskCommentValidatorTests
{
    private readonly UpdateTaskCommentValidator _validator = new UpdateTaskCommentValidator();

    [Fact]
    public void Should_PassValidation_WhenRequestIsValid()
    {
        var request = new UpdateTaskCommentRequest
        {
            Comment = "Comentário atualizado"
        };
        request.UpdateId(Guid.NewGuid());
        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_FailValidation_WhenIdIsEmpty()
    {
        var request = new UpdateTaskCommentRequest
        {
            Comment = "Comentário válido"
        };
        request.UpdateId(Guid.Empty);

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Id");
    }

    [Fact]
    public void Should_FailValidation_WhenCommentIsEmpty()
    {
        var request = new UpdateTaskCommentRequest
        {
            Comment = ""
        };
        request.UpdateId(Guid.NewGuid());

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Comment");
    }
}