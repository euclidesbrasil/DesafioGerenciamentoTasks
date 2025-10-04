using AutoMapper;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.Project.FilterById;
namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterById;
public sealed class FilterProjectByIdHandler : IRequestHandler<FilterProjectByIdRequest, FilterProjectByIdResponse>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public FilterProjectByIdHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FilterProjectByIdResponse> Handle(FilterProjectByIdRequest query, CancellationToken cancellationToken)
    {
        Entities.Project entity = await _repository.Get(query.id, cancellationToken);
        if (entity is null)
        {
            throw new InvalidOperationException($"Project não encontrado. Id: {query.id}");
        }

        return entity.MapToResponse();
    }
} 