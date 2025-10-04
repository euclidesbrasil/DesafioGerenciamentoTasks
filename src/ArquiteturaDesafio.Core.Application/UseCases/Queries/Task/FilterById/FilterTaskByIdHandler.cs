using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterById;
public sealed class FilterTaskByIdHandler : IRequestHandler<FilterTaskByIdRequest, FilterTaskByIdResponse>
{
    private readonly ITaskRepository _repository;

    public FilterTaskByIdHandler(ITaskRepository repository)
    {
        _repository = repository;
    }
    public async Task<FilterTaskByIdResponse> Handle(FilterTaskByIdRequest query, CancellationToken cancellationToken)
    {
        Entities.Task entity = await _repository.GetTaskIncludeProject(query.id, cancellationToken);
        if (entity is null)
        {
            throw new InvalidOperationException($"Task não encontrado. Id: {query.id}");
        }

        return entity.MapToResponse();
    }
}
