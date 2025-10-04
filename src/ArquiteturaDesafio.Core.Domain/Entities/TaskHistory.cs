using ArquiteturaDesafio.Core.Domain.Common;
using ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.ValueObjects;

namespace ArquiteturaDesafio.Core.Domain.Entities;

public sealed class TaskHistory : BaseEntity
{
    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }
    public string BeforeValue { get; private set; } // Valor anterior
    public string AfterValue { get; private set; }  // Valor novo
    public TaskAction Type { get; private set; }  
    public Entities.Task Task { get; private set; }
    public Entities.User User { get; private set; }
    public TaskHistory(Guid id) { 
        Id = id;
    } // Para o EF Core
    private TaskHistory() { } // Para o EF Core

    public void SetHistoryInfo(Guid taskId, Guid userId, string beforeValue, string afterValue, TaskAction type)
    {
        TaskId = taskId;
        UserId = userId;
        BeforeValue = beforeValue;
        AfterValue = afterValue;
        Type = type;
    }
}