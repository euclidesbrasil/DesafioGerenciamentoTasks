using FluentValidation;

namespace ArquiteturaDesafio.Application.UseCases.Commands.User.DeleteProject;

public sealed class DeleteProjectValidator : AbstractValidator<DeleteProjectRequest>
{
    public DeleteProjectValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
