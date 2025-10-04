using System;
using Xunit;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.User.CreateProject;

namespace ArquiteturaDesafio.Tests.Application.Handlers.Validator;
public class CreateProjectValidatorTests
{
    private readonly CreateProjectValidator _validator = new CreateProjectValidator();

    [Fact]
    public void Should_PassValidation_WhenDataIsValid()
    {
        var request = new CreateProjectRequest("Projeto Teste", "Descrição do projeto", Guid.NewGuid());


        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_FailValidation_WhenTitleIsEmpty()
    {
        var request = new CreateProjectRequest(string.Empty, "Descrição do projeto", Guid.NewGuid());


        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }

    [Fact]
    public void Should_FailValidation_WhenDescriptionIsTooLong()
    {
        var request = new CreateProjectRequest("Projeto Teste", new string('x', 1001), Guid.NewGuid());
        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description");
    }
}