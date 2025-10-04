using AutoMapper;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;
using ArquiteturaDesafio.Core.Application.UseCases.DTOs;
using ArquiteturaDesafio.Core.Domain.Common;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterQuery
{ 
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQueryRequest, GetTasksQueryResponse>
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetTasksQueryResponse> Handle(GetTasksQueryRequest request, CancellationToken cancellationToken)
        {
            var sales = await _repository.GetTasksPagination(new PaginationQuery()
            {
                Order = request.order,
                Page = request.page,
                Size = request.size,
                Filter = request.filters
            }, cancellationToken);

            List<TaskBaseDTO> itensReturn = new List<TaskBaseDTO>();
            foreach(var item in sales.Data)
            {
                itensReturn.Add(item.MapToResponse());
            }

            return new GetTasksQueryResponse()
            {
                Data = itensReturn,
                CurrentPage = sales.CurrentPage,
                TotalItems = sales.TotalItems,
                TotalPages = sales.TotalPages
            };
        }
    }
}
