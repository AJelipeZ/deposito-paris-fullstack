using Microsoft.Extensions.DependencyInjection;

namespace DepositoParis.Application.Abstractions;

public class Dispatcher(IServiceProvider services) : IDispatcher
{
    public Task<TResultado> SendAsync<TResultado>(ICommand<TResultado> command, CancellationToken ct = default)
    {
        var tipoHandler = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResultado));
        dynamic handler = services.GetRequiredService(tipoHandler);
        return handler.HandleAsync((dynamic)command, ct);
    }

    public Task<TResultado> QueryAsync<TResultado>(IQuery<TResultado> query, CancellationToken ct = default)
    {
        var tipoHandler = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResultado));
        dynamic handler = services.GetRequiredService(tipoHandler);
        return handler.HandleAsync((dynamic)query, ct);
    }
}
