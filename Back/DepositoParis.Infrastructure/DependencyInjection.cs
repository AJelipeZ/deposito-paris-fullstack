using DepositoParis.Application.Abstractions;
using DepositoParis.Infrastructure.Persistence;
using DepositoParis.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DepositoParis.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var cadena = configuration.GetConnectionString("DepositoParis")
                     ?? "Data Source=deposito_paris.db;Foreign Keys=True";

        services.AddSingleton<IDbConnectionFactory>(new SqliteConnectionFactory(cadena));
        services.AddSingleton<DatabaseInitializer>();

        // Lectura (Queries)
        services.AddScoped<IRemisionReadRepository, RemisionReadRepository>();
        services.AddScoped<IArticuloReadRepository, ArticuloReadRepository>();
        services.AddScoped<IConductorReadRepository, ConductorReadRepository>();

        // Escritura (Commands)
        services.AddScoped<IRemisionWriteRepository, RemisionWriteRepository>();

        return services;
    }
}
