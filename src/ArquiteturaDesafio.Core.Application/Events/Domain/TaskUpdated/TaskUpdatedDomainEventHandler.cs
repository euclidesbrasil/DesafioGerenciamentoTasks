using ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using TarefaAsync = System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;

namespace ArquiteturaDesafio.Core.Application.Events.Domain.TaskUpdated;

public class TaskUpdatedDomainEventHandler : INotificationHandler<TaskUpdatedDomainEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskHistoryRepository _historyRepo;
    public TaskUpdatedDomainEventHandler(ITaskHistoryRepository historyRepo, IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
        _historyRepo = historyRepo;
    }

    public async TarefaAsync.Task Handle(TaskUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var history = new TaskHistory(Guid.NewGuid());
        history.SetHistoryInfo(
            notification.CorrelationId,
            notification.UserId,
            string.IsNullOrEmpty(notification.JsonBefore)? JsonConvert.SerializeObject(notification.Before): notification.JsonBefore,
            string.IsNullOrEmpty(notification.JsonAfter) ? JsonConvert.SerializeObject(notification.After) : notification.JsonAfter,
            notification.Type
        );

        _historyRepo.Create(history);
        // Commit será dado no Handler.
    }
}
