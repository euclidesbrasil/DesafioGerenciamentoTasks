using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Project.FilterQuery
{
    public sealed record GetProjectsQueryRequest(int page, int size, string order, Dictionary<string, string> filters = null) : IRequest<GetProjectsQueryResponse>;
}
