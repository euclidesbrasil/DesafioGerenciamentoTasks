using AutoMapper;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.CreateTask;

public class CreateTaskHandler :
       IRequestHandler<CreateTaskRequest, CreateTaskResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepository _repository;
    private readonly IMapper _mapper;

    public CreateTaskHandler(IUnitOfWork unitOfWork,
        ITaskRepository repository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateTaskResponse> Handle(CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ArquiteturaDesafio.Core.Domain.Entities.Task>(request);
        entity.setNewTask();
        await ValidateLimit(entity, cancellationToken);

        _repository.Create(entity);
        await _unitOfWork.Commit(cancellationToken);
        return new CreateTaskResponse(entity.Id);
    }

    private async System.Threading.Tasks.Task ValidateLimit(ArquiteturaDesafio.Core.Domain.Entities.Task entity, CancellationToken cancellationToken)
    {
        if (await _repository.LimitHasReachedByProject(entity.ProjectId, entity.Id, cancellationToken))
        {
            throw new ArgumentException("Não é possível gravar a tarefa. Cada projeto tem um limite máximo de 20 tarefas.");
        }

        return;
    }
}
