using FluentValidation;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.User.DeleteUser;
public sealed class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
{
    public DeleteUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
