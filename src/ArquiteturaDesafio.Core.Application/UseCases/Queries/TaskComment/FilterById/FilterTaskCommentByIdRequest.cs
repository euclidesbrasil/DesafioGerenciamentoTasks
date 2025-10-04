using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.TaskComment.FilterById;

public sealed record FilterTaskCommentByIdRequest(Guid id) : IRequest<FilterTaskCommentByIdResponse>;