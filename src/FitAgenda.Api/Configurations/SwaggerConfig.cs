using Microsoft.OpenApi;
using System.Reflection;

namespace FitAgenda.Api.Configurations;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FitAgenda API",
                Version = "v1",
                Description = "API para gerenciamento de alunos, aulas, agendamentos e relatorios da academia."
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);
        });

        return services;
    }

    public static WebApplication UseSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "FitAgenda API v1");
            options.RoutePrefix = "swagger";
            options.DocumentTitle = "FitAgenda API";
        });

        return app;
    }
}
