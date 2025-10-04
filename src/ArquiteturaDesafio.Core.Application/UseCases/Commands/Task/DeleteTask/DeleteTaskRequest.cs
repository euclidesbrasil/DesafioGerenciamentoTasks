using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.DeleteTask;
public sealed record DeleteTaskRequest(Guid Id) : IRequest<DeleteTaskResponse>
{
}
