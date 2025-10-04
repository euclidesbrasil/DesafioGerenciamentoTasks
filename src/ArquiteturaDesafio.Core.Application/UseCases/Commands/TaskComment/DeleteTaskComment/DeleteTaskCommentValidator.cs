using FluentValidation;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.DeleteTaskComment;
public sealed class DeleteTaskCommentValidator : AbstractValidator<DeleteTaskCommentRequest>
{
    public DeleteTaskCommentValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
