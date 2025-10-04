using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterById;

public sealed record FilterTaskByIdRequest(Guid id) : IRequest<FilterTaskByIdResponse>;