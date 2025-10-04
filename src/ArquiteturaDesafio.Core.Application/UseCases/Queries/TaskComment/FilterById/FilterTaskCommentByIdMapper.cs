namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.TaskComment.FilterById;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
public static class FilterTaskCommentByIdMapper
{
    public static FilterTaskCommentByIdResponse MapToResponse(this Entities.TaskComment task)
    {
        return new FilterTaskCommentByIdResponse
        {
            Id = task.Id,
            Comment = task.Comment,
            DateCreated = task.DateCreated,
            DateUpdated = task.DateUpdated
        };
    }
}
