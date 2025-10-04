using ArquiteturaDesafio.Core.Domain.Common;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Context;
using ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Extensions;
using ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Repositories
{
    public class TaskRepository : BaseRepository<Entities.Task>, ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<Entities.Task>> GetTasksPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken)
        {
            var query = _context.Tasks.Where(x => true).Include("Project");
            query = query.ApplyFilters(paginationQuery.Filter);
            paginationQuery.Order = string.IsNullOrEmpty(paginationQuery.Order) ? "id asc": paginationQuery.Order;
            query = query.OrderBy(paginationQuery.Order);

            var totalCount = await query.CountAsync(cancellationToken); // Total de itens sem paginação
            var items = await query
                .Skip(paginationQuery.Skip)
                .Take(paginationQuery.Size)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<Entities.Task>
            {
                Data = items,
                TotalItems = totalCount,
                CurrentPage = paginationQuery.Page
            };
        }

        public async Task<Entities.Task> GetTaskIncludeProject(Guid id,CancellationToken cancellationToken)
        {
            var query = _context.Tasks.Where(x => x.Id == id).Include("Project");
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Entities.Task>> ListAllDoneTaskIn30Days(CancellationToken cancellationToken)
        {
            var last30Days = DateTime.UtcNow.AddDays(-30);
            var query = _context.Tasks.Where(x =>
                x.Status == ArquiteturaDesafio.Core.Domain.Enum.TaskStatus.Done &&
                x.DateUpdated >= last30Days
            ).Include("ResponsableUser");

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<bool> HasAnyPendinigTaskByProject(Guid idProject, CancellationToken cancellationToken)
        { 
            var query = _context.Tasks.Where(x => x.ProjectId == idProject && x.Status != ArquiteturaDesafio.Core.Domain.Enum.TaskStatus.Done) ;
            var totalCount = await query.CountAsync(cancellationToken); // Total de itens sem paginação
            return totalCount > 0;
        }

        public async Task<bool> LimitHasReachedByProject(Guid idProject, Guid idTask, CancellationToken cancellationToken)
        {
            // Todas as tarefas do projeto, sem contar a propria tarefa em questão.
            var query = _context.Tasks.Where(x => x.ProjectId == idProject && x.Id != idTask);
            var totalCount = await query.CountAsync(cancellationToken); // Total de itens sem paginação
            return totalCount >= 20;
        }
    }
}
