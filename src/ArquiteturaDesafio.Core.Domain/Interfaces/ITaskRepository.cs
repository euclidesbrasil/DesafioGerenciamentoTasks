using ArquiteturaDesafio.Core.Domain.Common;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArquiteturaDesafio.Core.Domain.Interfaces
{
    public interface ITaskRepository : IBaseRepository<Entities.Task>
    {
        Task<PaginatedResult<Entities.Task>> GetTasksPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken);
        Task<bool> HasAnyPendinigTaskByProject(Guid idProject, CancellationToken cancellationToken);
        Task<bool> LimitHasReachedByProject(Guid idProject, Guid idTask, CancellationToken cancellationToken);
        Task<List<Entities.Task>> ListAllDoneTaskIn30Days(CancellationToken cancellationToken);
        Task<Entities.Task> GetTaskIncludeProject(Guid id, CancellationToken cancellationToken);
    }
}
