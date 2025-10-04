using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.TaskComment.FilterById;
public sealed class FilterTaskCommentByIdHandler : IRequestHandler<FilterTaskCommentByIdRequest, FilterTaskCommentByIdResponse>
{
    private readonly ITaskCommentRepository _repository;

    public FilterTaskCommentByIdHandler(ITaskCommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<FilterTaskCommentByIdResponse> Handle(FilterTaskCommentByIdRequest query, CancellationToken cancellationToken)
    {
        Entities.TaskComment entity = await _repository.Get(query.id, cancellationToken);
        if (entity is null)
        {
            throw new InvalidOperationException($"TaskComment não encontrado. Id: {query.id}");
        }

        return entity.MapToResponse();
    }
}
