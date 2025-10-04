using Enum = ArquiteturaDesafio.Core.Domain.Enum;
using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.CreateTask
{
    public class CreateTaskRequest : IRequest<CreateTaskResponse>
    {
        public Guid ProjectId { get; set; }
        public Guid Id { get; internal set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateExpiration { get; set; }
        public Enum.TaskPriority Priority { get; set; }
        public Guid ResponsableUserId { get; set; }
    }
}
