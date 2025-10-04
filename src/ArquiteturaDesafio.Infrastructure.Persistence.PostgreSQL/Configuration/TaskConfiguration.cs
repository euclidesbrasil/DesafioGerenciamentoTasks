using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;

namespace ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Entities.Task> builder)
        {
            // Nome da Tabela
            builder.ToTable("Tasks");

            // Definir Chave Primária
            builder.HasKey(u => u.Id);

            // Configurar Campos
            builder.Property(u => u.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(u => u.DateExpiration)
                .IsRequired();

            builder.Property(u => u.ProjectId)
                .IsRequired();

            // Enum armazenado como string (opcional)
            builder.Property(u => u.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.HasOne(t => t.Project)
                .WithMany()
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(t => t.ResponsableUser)
              .WithMany()
              .HasForeignKey(t => t.ResponsableUserId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(t => t.Historys)
              .WithOne(h => h.Task)
              .HasForeignKey(h => h.TaskId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Comments)
              .WithOne(h => h.Task)
              .HasForeignKey(c => c.TaskId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
