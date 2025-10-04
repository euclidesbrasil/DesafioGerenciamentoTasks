using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Context;
using ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using ArquiteturaDesafio.Core.Application.UseCases.Mapper;
using ArquiteturaDesafio.Core.Application.Shared.Behavior;
using ArquiteturaDesafio.Infrastructure.Security;
namespace ArquiteturaDesafio.Infrastructure.CrossCutting.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Postgres
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
            services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<JwtTokenService>();
            services.AddSingleton<IJwtTokenService, JwtTokenService>();
            services.AddAutoMapper(typeof(CommonMapper));

            var myhandlers = AppDomain.CurrentDomain.Load("ArquiteturaDesafio.Core.Application");
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(myhandlers);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.Load("ArquiteturaDesafio.Core.Application"));

            return services;
        }
    }
}
