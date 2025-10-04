using ArquiteturaDesafio.Core.Domain.Enum;
using FluentValidation;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.CreateTask;

public sealed class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(1).MaximumLength(1000);
        RuleFor(x => x.DateExpiration).NotEmpty().GreaterThan(DateTime.Now);
        RuleFor(x => x.ResponsableUserId).NotEmpty();
        RuleFor(x => x.Priority).Must(p => Enum.IsDefined(typeof(TaskPriority), p) && p != (int)TaskPriority.None).WithMessage("A prioridade deve ser válida. - Low = 1, Medium = 2, Hight = 3 ");
    }
    
}
