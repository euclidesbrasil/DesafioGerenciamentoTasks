using ArquiteturaDesafio.Core.Domain.Common;
using ArquiteturaDesafio.Core.Domain.Entities;
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
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly AppDbContext _context;
        public ProjectRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<Project>> GetProjectsPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken)
        {
            var query = _context.Projects.Where(x => true);
            query = query.ApplyFilters(paginationQuery.Filter);
            paginationQuery.Order = paginationQuery.Order ?? "id asc";
            query = query.OrderBy(paginationQuery.Order);

            var totalCount = await query.CountAsync(cancellationToken); // Total de itens sem paginação
            var items = await query
                .Skip(paginationQuery.Skip)
                .Take(paginationQuery.Size)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<Project>
            {
                Data = items,
                TotalItems = totalCount,
                CurrentPage = paginationQuery.Page
            };
        }

    }
}
