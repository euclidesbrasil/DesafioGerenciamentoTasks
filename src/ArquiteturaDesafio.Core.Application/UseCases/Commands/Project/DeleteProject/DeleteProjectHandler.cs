using AutoMapper;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Application.UseCases.Commands.User.DeleteProject;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectRequest, DeleteProjectResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectRepository _repository;
    private readonly ITaskRepository _repositoryTask;
    private readonly IMapper _mapper;

    public DeleteProjectHandler(IUnitOfWork unitOfWork,
        IProjectRepository repository,
        ITaskRepository repositoryTask,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _repositoryTask = repositoryTask;
        _mapper = mapper;
    }

    public async Task<DeleteProjectResponse> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
    {
        Entities.Project entity = await _repository.Get(request.Id, cancellationToken);

        if (entity is null)
        {
            throw new KeyNotFoundException($"Projeto não encontrado. Id: {request.Id}");
        }
        await ValidateDeleteProject(entity, cancellationToken);
        _repository.Delete(entity);
        await _unitOfWork.Commit(cancellationToken);
        return new DeleteProjectResponse();
    }

    private async Task ValidateDeleteProject(Entities.Project entity, CancellationToken cancellationToken)
    {
        if (await _repositoryTask.HasAnyPendinigTaskByProject(entity.Id, cancellationToken))
        {
            throw new ArgumentException("Não é possível excluir o projeto, pois existem tarefas pendentes associadas a ele.");
        }

        return;
    }

}
