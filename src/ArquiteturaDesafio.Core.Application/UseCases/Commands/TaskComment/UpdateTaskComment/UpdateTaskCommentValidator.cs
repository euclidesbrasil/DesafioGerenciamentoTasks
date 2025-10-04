using FluentValidation;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTaskComment;
public sealed class UpdateTaskCommentValidator : AbstractValidator<UpdateTaskCommentRequest>
{
    public UpdateTaskCommentValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Comment).NotEmpty();
    }
}
