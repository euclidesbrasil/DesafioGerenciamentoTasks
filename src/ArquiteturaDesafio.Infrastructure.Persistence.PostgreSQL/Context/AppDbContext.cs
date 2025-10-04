using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Context;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Entities.User> Users { get; set; }
    public DbSet<Entities.Project> Projects { get; set; }
    public DbSet<Entities.Task> Tasks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new TaskHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new TaskCommentConfiguration());
    }
}

