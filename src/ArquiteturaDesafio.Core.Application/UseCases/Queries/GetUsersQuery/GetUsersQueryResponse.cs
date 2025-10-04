using ArquiteturaDesafio.Core.Application.UseCases.DTOs;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.GetUsersQuery
{
    public sealed record GetUsersQueryResponse
    {
        public List<UserDTO> Data { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
