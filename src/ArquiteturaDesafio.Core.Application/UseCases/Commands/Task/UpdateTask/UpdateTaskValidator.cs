using FluentValidation;
using Enums = ArquiteturaDesafio.Core.Domain.Enum;
namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTask;
public sealed class UpdateTaskValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Status).Must(p => Enum.IsDefined(typeof(Enums.TaskStatus), p) && p != (int)Enums.TaskStatus.None).WithMessage("O Status deve ser válido. - Pending = 1, Doing = 2, Done = 3 ");
    }
}
