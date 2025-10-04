using FluentValidation;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.User.CreateProject;

public sealed class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(1).MaximumLength(1000);
    }
}
