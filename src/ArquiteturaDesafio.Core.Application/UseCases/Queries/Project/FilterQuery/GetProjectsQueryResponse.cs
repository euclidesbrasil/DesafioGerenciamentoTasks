using ArquiteturaDesafio.Core.Application.UseCases.DTOs;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Project.FilterQuery
{
    public sealed record GetProjectsQueryResponse
    {
        public List<ProjectBaseDTO> Data { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
