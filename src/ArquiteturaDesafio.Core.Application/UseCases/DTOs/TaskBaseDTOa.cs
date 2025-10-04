using Enum = ArquiteturaDesafio.Core.Domain.Enum;
namespace ArquiteturaDesafio.Core.Application.UseCases.DTOs
{
    public class TaskBaseDTO
    {
        public Guid Id { get; set; }
        public string Title { get;  set; }
        public string Description { get;  set; }
        public DateTime DateExpiration { get;  set; }
        public Enum.TaskStatus Status { get;  set; }
        public Guid ProjectId { get;  set; }
        public Guid ResponsableUserId { get;  set; }
        public string ProjectName { get;  set; }
    }
}