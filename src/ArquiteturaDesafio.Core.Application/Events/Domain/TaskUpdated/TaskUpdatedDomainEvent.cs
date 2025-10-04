using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using MediatR;
using ArquiteturaDesafio.Core.Domain.Enum;

namespace ArquiteturaDesafio.Core.Application.Events.Domain.TaskUpdated;
//
public class TaskUpdatedDomainEvent : INotification
{
    public Guid CorrelationId { get; }
    public Entities.Task After { get; }
    public string JsonAfter { get; }
    public string JsonBefore { get; }
    public Entities.Task Before { get; }
    public TaskAction Type { get; }
    public Guid UserId { get; }

    public TaskUpdatedDomainEvent(Entities.Task after, Entities.Task before, Guid userId, Guid correlationId, TaskAction action)
    {
        After = after;
        Before = before;
        UserId = userId;
        CorrelationId = correlationId;
        Type = action;
    }
    public TaskUpdatedDomainEvent(string after, string before, Guid userId, Guid correlationId, TaskAction action)
    {
        JsonAfter = after;
        JsonBefore = before;
        UserId = userId;
        CorrelationId = correlationId;
        Type = action;
    }
}