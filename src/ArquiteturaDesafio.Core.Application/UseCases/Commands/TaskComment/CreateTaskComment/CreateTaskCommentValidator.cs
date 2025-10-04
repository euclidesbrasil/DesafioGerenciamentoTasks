 using FluentValidation;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.TaskComment.CreateTaskComment;
public sealed class CreateTaskCommentValidator : AbstractValidator<CreateTaskCommentRequest>
{
    public CreateTaskCommentValidator()
    {
        RuleFor(x => x.TaskId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
