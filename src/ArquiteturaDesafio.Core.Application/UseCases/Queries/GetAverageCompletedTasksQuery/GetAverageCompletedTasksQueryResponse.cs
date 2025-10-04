namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.GetAverageCompletedTasksQuery;
public class GetAverageCompletedTasksQueryResponse
{
    public decimal Avarage { get; set; }
    public IEnumerable<DetailCompletedTasksByUser> Details { get; set; }
}

public class DetailCompletedTasksByUser
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public int TotalCompleted { get; set; }
}