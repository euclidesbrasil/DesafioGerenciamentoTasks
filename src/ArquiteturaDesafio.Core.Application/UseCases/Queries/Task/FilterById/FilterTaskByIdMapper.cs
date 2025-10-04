namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterById;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
public static class FilterTaskByIdMapper 
{
    public static FilterTaskByIdResponse MapToResponse(this Entities.Task task)
    {
        return new FilterTaskByIdResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DateExpiration = task.DateExpiration,
            Status = task.Status,
            ProjectId = task.ProjectId,
            ProjectName = task.Project.Description
        };
    }
}
