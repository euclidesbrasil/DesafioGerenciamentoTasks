using AutoMapper;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;
namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.GetAverageCompletedTasksQuery;
public sealed class GetUsersByIdHandler : IRequestHandler<GetAverageCompletedTasksQueryRequest, GetAverageCompletedTasksQueryResponse>
{
    private readonly ITaskRepository _repository;
    private readonly IMapper _mapper;

    public GetUsersByIdHandler(ITaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<GetAverageCompletedTasksQueryResponse> Handle(GetAverageCompletedTasksQueryRequest query, CancellationToken cancellationToken)
    {

        var allTasks = await _repository.ListAllDoneTaskIn30Days(cancellationToken);
        allTasks = allTasks ?? new List<Domain.Entities.Task>();
        var details = allTasks.GroupBy(x => x.ResponsableUserId);
        
        var detailListByUser = details.Select(x => new DetailCompletedTasksByUser()
        {
            Name = x.FirstOrDefault()?.ResponsableUser?.Firstname,
            UserId = x.Key,
            TotalCompleted = x.Count(),
        });

        decimal avarage = detailListByUser.Any()
            ? allTasks.Count() / (detailListByUser.Count()*1.0M)
            : 0.0M;

        GetAverageCompletedTasksQueryResponse report = new GetAverageCompletedTasksQueryResponse();
        report.Avarage = avarage;
        report.Details = detailListByUser;
        
        return report;
    }
}