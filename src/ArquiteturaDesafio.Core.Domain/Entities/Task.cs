using System.Linq;
using ArquiteturaDesafio.Core.Domain.Common;
using ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.ValueObjects;

namespace ArquiteturaDesafio.Core.Domain.Entities;

public sealed class Task : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime DateExpiration { get; private set; }
    public Enum.TaskStatus Status { get; private set; }
    public Enum.TaskPriority Priority { get; private set; }
    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; }
    public IEnumerable<TaskHistory> Historys { get; private set; }
    public IEnumerable<TaskComment> Comments { get; private set; }

    public Guid ResponsableUserId { get; private set; }
    public User ResponsableUser { get; private set; }

    public void setNewTask()
    {
        Id = Guid.NewGuid();
        Status = Enum.TaskStatus.Pending;
    }
    private Task() { } // Para o EF Core

    public Task(Enum.TaskStatus status )
    {
        Id = Guid.NewGuid();
        Status = status;
    }

    public void SetPriority(Enum.TaskPriority priority)
    {
        Priority = priority;
    }

    public void SetResponsableUser(User user)
    {
        ResponsableUser = user;
    }

    public void SetResponsableUserId(Guid id)
    {
        ResponsableUserId = id;
    }

    public void SetProjectId(Guid projectId)
    {
        ProjectId = projectId;
    }

    public void UpdateTaskInfo(string title, string desc, DateTime dateExpiration, Enum.TaskStatus status)
    {
        Description = desc;
        Title = title;
        DateExpiration = dateExpiration;
        Status = status;
    }

    public void AssociateToProject(Guid idProj)
    {
        ProjectId = idProj;
    }

    public void ValidadeTaskRules(Task before)
    {
        if(before.Priority != this.Priority)
        {
            throw new ArgumentException("The priority can't be changed.");
        }
    }
}