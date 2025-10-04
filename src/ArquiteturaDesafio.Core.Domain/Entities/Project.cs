using ArquiteturaDesafio.Core.Domain.Common;
using ArquiteturaDesafio.Core.Domain.Enum;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.ValueObjects;

namespace ArquiteturaDesafio.Core.Domain.Entities;

public sealed class Project : BaseEntity
{
    public Guid UserId { get; private set; }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public ICollection<Task> Tasks { get; private set; }
    public User User { get; private set; }

    public Project(Guid id)
    {
        Id = id;
    }
    private Project() { } // Para o EF Core

    public void UpdateInfo(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public void SetUser(Guid userId)
    {
        UserId = userId;
    }


    public void AddTasks(ICollection<Task> tasks)
    {
        this.Tasks = this.Tasks ?? new List<Task>();

        foreach (var item in tasks)
        {
            this.Tasks.Add(item);
        }
    }
}