using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.DeleteTaskComment;

public sealed record DeleteTaskCommentRequest(Guid Id) : IRequest<DeleteTaskCommentResponse>
{
}
