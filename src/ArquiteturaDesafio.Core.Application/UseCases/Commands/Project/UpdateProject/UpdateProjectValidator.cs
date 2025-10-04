using FluentValidation;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Project.UpdateProject;

public sealed class UpdateProjectValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
    }
}
