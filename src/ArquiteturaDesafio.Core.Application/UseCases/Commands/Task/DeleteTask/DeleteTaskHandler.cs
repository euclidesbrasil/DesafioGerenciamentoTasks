using AutoMapper;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.DeleteTask;
public class DeleteTaskHandler : IRequestHandler<DeleteTaskRequest, DeleteTaskResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepository _repository;
    private readonly IMapper _mapper;

    public DeleteTaskHandler(IUnitOfWork unitOfWork,
        ITaskRepository repository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DeleteTaskResponse> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
    {
        Entities.Task entity = await _repository.Get(request.Id, cancellationToken);

        if (entity is null)
        {
            throw new KeyNotFoundException($"Task não encontrado. Id: {request.Id}");
        }

        _repository.Delete(entity);
        await _unitOfWork.Commit(cancellationToken);
        return new DeleteTaskResponse();
    }
}
