using MediatR;

namespace ArquiteturaDesafio.Application.UseCases.Commands.User.DeleteProject;
public sealed record DeleteProjectRequest(Guid Id) : IRequest<DeleteProjectResponse>
{
}
