using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.User.CreateProject
{
    public record CreateProjectRequest(string Title, string Description, Guid UserId) : IRequest<CreateProjectResponse>
    {
    }
}
