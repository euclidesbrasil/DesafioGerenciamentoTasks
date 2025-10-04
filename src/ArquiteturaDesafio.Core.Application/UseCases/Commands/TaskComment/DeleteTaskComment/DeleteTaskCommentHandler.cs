using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.DeleteTaskComment;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommentRequest, DeleteTaskCommentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskCommentRepository _repository;

    public DeleteTaskHandler(IUnitOfWork unitOfWork,
        ITaskCommentRepository repository
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<DeleteTaskCommentResponse> Handle(DeleteTaskCommentRequest request, CancellationToken cancellationToken)
    {
        Entities.TaskComment entity = await _repository.Get(request.Id, cancellationToken);

        if (entity is null)
        {
            throw new KeyNotFoundException($"TaskComment não encontrado. Id: {request.Id}");
        }

        _repository.Delete(entity);
        await _unitOfWork.Commit(cancellationToken);
        return new DeleteTaskCommentResponse();
    }
}
