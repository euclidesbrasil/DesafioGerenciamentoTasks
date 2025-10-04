using ArquiteturaDesafio.Core.Application.UseCases.DTOs;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterQuery
{
    public sealed record GetTasksQueryResponse
    {
        public List<TaskBaseDTO> Data { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
