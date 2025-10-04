using MediatR;
using Enum = ArquiteturaDesafio.Core.Domain.Enum;
namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTask
{
    public class UpdateTaskRequest : IRequest<UpdateTaskResponse>
    {
        public void PopulateRequest(Guid id, Guid idIUserContext, string title, string description, Guid idProject, DateTime dateExpiration, Enum.TaskStatus status)
        {
            Id = id;
            IdIUserContext = idIUserContext;
            Title = title;
            Description = description;
            IdProject = idProject;
            DateExpiration = dateExpiration;
            Status = status;
        }

        public Guid Id { get; internal set; }
        public Guid IdIUserContext { get; internal set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid IdProject { get; set; }
        public DateTime DateExpiration { get; set; }
        public Enum.TaskStatus Status { get; set; }

        public void UpdateId(Guid id)
        {
            Id = id;
        }

        public void SetUser(Guid id)
        {
            IdIUserContext = id;
        }
    }
}
