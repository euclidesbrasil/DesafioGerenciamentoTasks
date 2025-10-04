using ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Context;

namespace ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Repositories
{
    public class TaskHistoryRepository : BaseRepository<TaskHistory>, ITaskHistoryRepository
    {
        private readonly AppDbContext _context;
        public TaskHistoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
