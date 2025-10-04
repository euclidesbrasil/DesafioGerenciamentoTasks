using ArquiteturaDesafio.Core.Application.UseCases.DTOs;
using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Project.UpdateProject
{
    public class UpdateProjectRequest : IRequest<UpdateProjectResponse>
    {
        public Guid Id { get; internal set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
