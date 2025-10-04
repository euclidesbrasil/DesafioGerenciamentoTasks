using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.GetUsersQuery
{
    public sealed record GetUsersQueryRequest(int page, int size, string order, Dictionary<string, string> filters = null) : IRequest<GetUsersQueryResponse>;
}
