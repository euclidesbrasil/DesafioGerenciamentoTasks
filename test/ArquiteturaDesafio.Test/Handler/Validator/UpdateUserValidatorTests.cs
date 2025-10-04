using System;
using Xunit;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.User.UpdateUser;
using ArquiteturaDesafio.Core.Domain.Enum;

namespace ArquiteturaDesafio.Tests.Application.Handlers.Validator;
public class UpdateUserValidatorTests
{
    private readonly UpdateUserValidator _validator = new UpdateUserValidator();

    [Fact]
    public void Should_PassValidation_WhenRequestIsValid()
    {
        var request = new UpdateUserRequest
        {
            Username = "euclides.lima",
            Email = "euclides@email.com",
            Role = UserRole.Manager
        };
        request.UpdateId(Guid.NewGuid());
        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_Fail_WhenIdIsEmpty()
    {
        var request = new UpdateUserRequest
        {
            Username = "validuser",
            Email = "valid@email.com",
            Role = UserRole.Admin
        };
        request.UpdateId(Guid.Empty);

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Id");
    }

    [Fact]
    public void Should_Fail_WhenUsernameIsEmpty()
    {
        var request = new UpdateUserRequest
        {
            Username = "",
            Email = "valid@email.com",
            Role = UserRole.Customer
        };
        request.UpdateId(Guid.NewGuid());

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Username");
    }

    [Fact]
    public void Should_Fail_WhenEmailIsInvalid()
    {
        var request = new UpdateUserRequest
        {
            Username = "validuser",
            Email = "invalid-email",
            Role = UserRole.Manager
        };
        request.UpdateId(Guid.NewGuid());

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public void Should_Fail_WhenRoleIsNone()
    {
        var request = new UpdateUserRequest
        {
            Username = "validuser",
            Email = "valid@email.com",
            Role = UserRole.None
        };
        request.UpdateId(Guid.NewGuid());

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Role");
    }
}