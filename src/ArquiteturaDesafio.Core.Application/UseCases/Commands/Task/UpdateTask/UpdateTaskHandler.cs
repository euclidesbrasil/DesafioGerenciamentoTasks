using AutoMapper;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;
using ArquiteturaDesafio.Core.Application.Events.Domain.TaskUpdated;
using ArquiteturaDesafio.Core.Domain.Enum;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTask;

public class UpdateTaskHandler :
       IRequestHandler<UpdateTaskRequest, UpdateTaskResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepository _repository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public UpdateTaskHandler(IUnitOfWork unitOfWork,
        ITaskRepository userRepository,
        IMediator mediator,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _repository = userRepository;
        _mediator   = mediator;
        _mapper = mapper;
    }


    public async Task<UpdateTaskResponse> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var entity = await _repository.Get(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new InvalidOperationException($"Task não encontrado. Id: {request.Id}");
        }
        var before = _mapper.Map<Entities.Task>(entity);
        
        entity.UpdateTaskInfo(request.Title, request.Description, request.DateExpiration, request.Status);
        entity.AssociateToProject(request.IdProject);
        
        // Validações de regras de negócio.
        entity.ValidadeTaskRules(before);
        await ValidateLimit(entity, cancellationToken);
        
        _repository.Update(entity);

        // Gerar evento de dominio, gravando a mudança de histórico.
        var taskUpdatedEvent = new TaskUpdatedDomainEvent(entity, before, request.IdIUserContext,request.Id, TaskAction.UpdateTask);
        await _mediator.Publish(taskUpdatedEvent, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return new UpdateTaskResponse();
    }

    private async System.Threading.Tasks.Task ValidateLimit(Entities.Task entity, CancellationToken cancellationToken)
    {
        if (await _repository.LimitHasReachedByProject(entity.ProjectId,entity.Id, cancellationToken))
        {
            throw new ArgumentException("Não é possível gravar a tarefa. Cada projeto tem um limite máximo de 20 tarefas.");
        }

        return;
    }
}
