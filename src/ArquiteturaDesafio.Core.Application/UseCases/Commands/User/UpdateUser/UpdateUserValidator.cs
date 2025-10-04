using ArquiteturaDesafio.Core.Domain.Enum;
using FluentValidation;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.User.UpdateUser;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Username).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Role).Must(p => Enum.IsDefined(typeof(UserRole), p) && p != (int)UserRole.None).WithMessage("A role deve ser válida. - Customer = 1, Manager = 2, Admin = 3 ");
    }
} 