using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;
using ArquiteturaDesafio.Core.Application.Events.Domain.TaskUpdated;
using ArquiteturaDesafio.Core.Domain.Enum;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.TaskComment.CreateTaskComment;
public class CreateTaskCommentHandler :
       IRequestHandler<CreateTaskCommentRequest, CreateTaskCommentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskCommentRepository _repository;
    private readonly IMediator _mediator;

    public CreateTaskCommentHandler(IUnitOfWork unitOfWork,
        IMediator mediator,
        ITaskCommentRepository repository
        )
    {
        _unitOfWork = unitOfWork;
        _mediator   = mediator;
        _repository = repository;
    }

    public async Task<CreateTaskCommentResponse> Handle(CreateTaskCommentRequest request,
        CancellationToken cancellationToken)
    {
        var entity = new Entities.TaskComment(Guid.NewGuid());
        entity.SetData(request.TaskId, request.UserId, request.Comment);
        _repository.Create(entity);

        // Gerar evento de dominio, gravando a mudança de histórico.
        var taskUpdatedEvent = new TaskUpdatedDomainEvent("", request.Comment, request.UserId, entity.TaskId, TaskAction.AddComment);
        await _mediator.Publish(taskUpdatedEvent, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
        return new CreateTaskCommentResponse(entity.Id);
    }
}
