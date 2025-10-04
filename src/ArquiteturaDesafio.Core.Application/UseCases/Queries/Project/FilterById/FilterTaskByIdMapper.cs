namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Project.FilterById;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
public static class FilterProjectByIdMapper
{
    public static FilterProjectByIdResponse MapToResponse(this Entities.Project project)
    {
        return new FilterProjectByIdResponse
        {
            Id = project.Id,
            UserId = project.UserId,
            Title = project.Title,
            Description = project.Description,
        };
    }
}
