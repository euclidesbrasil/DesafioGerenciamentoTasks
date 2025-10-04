using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Project.FilterById;

public sealed record FilterProjectByIdRequest(Guid id) : IRequest<FilterProjectByIdResponse>;