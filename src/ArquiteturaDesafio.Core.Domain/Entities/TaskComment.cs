using ArquiteturaDesafio.Core.Domain.Common;
using ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.ValueObjects;

namespace ArquiteturaDesafio.Core.Domain.Entities;

public sealed class TaskComment : BaseEntity
{
    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }
    public string Comment { get; private set; } 
    public Entities.Task Task { get; private set; }
    public Entities.User User { get; private set; }

    public TaskComment(Guid id)
    {
        Id = id;
    }

    private TaskComment() { } // Para o EF Core

    public void UpdateComment(string comment)
    {
        Comment = comment;
    }
    public void SetData(Guid taskId, Guid userId, string comment)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        TaskId = taskId;
        Comment = comment;
    }
}