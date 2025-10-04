 using FluentValidation;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.DeleteTask;
public sealed class DeleteTaskValidator : AbstractValidator<DeleteTaskRequest>
{
    public DeleteTaskValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
