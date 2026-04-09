using FitAgenda.Data.Context;
using FitAgenda.Data.Repositories;
using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Notifications;
using FitAgenda.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace FitAgenda.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<INotificador, Notificador>();

        services.AddScoped<IAlunoRepository, AlunoRepository>();
        services.AddScoped<IAulaRepository, AulaRepository>();
        services.AddScoped<ITipoAulaRepository, TipoAulaRepository>();
        services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
        services.AddScoped<IRelatorioRepository, RelatorioRepository>();

        services.AddScoped<IAlunoService, AlunoService>();
        services.AddScoped<IAulaService, AulaService>();
        services.AddScoped<ITipoAulaService, TipoAulaService>();
        services.AddScoped<IAgendamentoService, AgendamentoService>();
        services.AddScoped<IRelatorioService, RelatorioService>();

        return services;
    }
}
