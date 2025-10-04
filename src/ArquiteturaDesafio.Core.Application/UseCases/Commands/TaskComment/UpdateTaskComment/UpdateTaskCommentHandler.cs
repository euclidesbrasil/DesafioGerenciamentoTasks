using ArquiteturaDesafio.Core.Application.Events.Domain.TaskUpdated;
using ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTaskComment;

public class UpdateTaskCommentHandler :
       IRequestHandler<UpdateTaskCommentRequest, UpdateTaskCommentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskCommentRepository _repository;
    private readonly IMediator _mediator;

    public UpdateTaskCommentHandler(IUnitOfWork unitOfWork,
        IMediator mediator,
        ITaskCommentRepository repository
        )
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<UpdateTaskCommentResponse> Handle(UpdateTaskCommentRequest request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.Get(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new InvalidOperationException($"TaskComment não encontrado. Id: {request.Id}");
        }
        var before = entity.Comment;

        entity.UpdateComment(request.Comment);
        _repository.Update(entity);

        var taskUpdatedEvent = new TaskUpdatedDomainEvent(request.Comment, before, request.UserId, entity.TaskId, TaskAction.EditComment);
        await _mediator.Publish(taskUpdatedEvent, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
        return new UpdateTaskCommentResponse();
    }
}
