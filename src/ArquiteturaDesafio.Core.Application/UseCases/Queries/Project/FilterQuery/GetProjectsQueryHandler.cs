using AutoMapper;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;
using ArquiteturaDesafio.Core.Application.UseCases.DTOs;
using ArquiteturaDesafio.Core.Domain.Common;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Project.FilterQuery
{ 
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQueryRequest, GetProjectsQueryResponse>
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public GetProjectsQueryHandler(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetProjectsQueryResponse> Handle(GetProjectsQueryRequest request, CancellationToken cancellationToken)
        {
            var sales = await _repository.GetProjectsPagination(new PaginationQuery()
            {
                Order = request.order,
                Page = request.page,
                Size = request.size,
                Filter = request.filters
            }, cancellationToken);

            List<ProjectBaseDTO> itensReturn = new List<ProjectBaseDTO>();
            foreach(var item in sales.Data)
            {
                itensReturn.Add(item.MapToResponse());
            }

            return new GetProjectsQueryResponse()
            {
                Data = itensReturn,
                CurrentPage = sales.CurrentPage,
                TotalItems = sales.TotalItems,
                TotalPages = sales.TotalPages
            };
        }
    }
}
