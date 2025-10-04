using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.GetAverageCompletedTasksQuery;

public sealed record GetAverageCompletedTasksQueryRequest() : IRequest<GetAverageCompletedTasksQueryResponse>;