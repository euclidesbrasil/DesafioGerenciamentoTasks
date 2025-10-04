using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.ValueObjects;
using System.Net;
using System.Reflection.Emit;

namespace ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Configuration
{
    public class TaskHistoryConfiguration : IEntityTypeConfiguration<TaskHistory>
    {
        public void Configure(EntityTypeBuilder<TaskHistory> builder)
        {
            // Nome da Tabela
            builder.ToTable("TasksHistory");

            // Definir Chave Primária
            builder.HasKey(u => u.Id);

            // Configurar Campos
            builder.Property(u => u.BeforeValue)
                .IsRequired();

            builder.Property(u => u.AfterValue)
                .IsRequired();

            builder.Property(u => u.Type)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany() 
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Task)
                .WithMany()
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
