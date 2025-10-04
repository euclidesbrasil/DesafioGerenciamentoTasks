using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.ValueObjects;
using System.Net;
using System.Reflection.Emit;

namespace ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            // Nome da Tabela
            builder.ToTable("Projects");

            // Definir Chave Primária
            builder.HasKey(u => u.Id);

            // Configurar Campos
            builder.Property(u => u.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Description)
                .IsRequired()
                .HasMaxLength(1000);

            // Mapeamento do relacionamento 1:N (Project -> Tasks)
            builder.HasMany(p => p.Tasks)
                   .WithOne(t => t.Project)
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(t => t.User)
                .WithMany() 
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
