using DepositoParis.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DepositoParis.Application;

public static class DependencyInjection
{
    /// <summary>Registra el despachador y todos los manejadores de comandos y consultas.</summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        var handlers = typeof(DependencyInjection).Assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            (i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                             i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                .Select(i => (Interfaz: i, Implementacion: t)));

        foreach (var (interfaz, implementacion) in handlers)
        {
            services.AddScoped(interfaz, implementacion);
        }

        return services;
    }
}
