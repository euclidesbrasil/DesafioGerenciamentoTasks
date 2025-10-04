namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Project.FilterQuery;

using ArquiteturaDesafio.Core.Application.UseCases.DTOs;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using Enum = ArquiteturaDesafio.Core.Domain.Enum;
public static class GetProjectsQueryMapper
{
    public static ProjectBaseDTO MapToResponse(this Entities.Project Project)
    {
        return new ProjectBaseDTO
        {
            Id = Project.Id,
            Title = Project.Title,
            Description = Project.Description,
        };
    }
}
