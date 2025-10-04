namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterQuery;

using ArquiteturaDesafio.Core.Application.UseCases.DTOs;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
public static class GetTasksQueryMapper
{
    public static TaskBaseDTO MapToResponse(this Entities.Task task)
    {
        return new TaskBaseDTO
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DateExpiration = task.DateExpiration,
            Status = task.Status,
            ProjectId = task.ProjectId,
            ProjectName = task.Project?.Description,
            ResponsableUserId = task.ResponsableUserId
        };
    }
}
