namespace DepositoParis.Application.Abstractions;

// CQRS: las escrituras (Commands) y las lecturas (Queries) van por caminos separados.

public interface ICommand<TResultado>;

public interface ICommandHandler<in TCommand, TResultado> where TCommand : ICommand<TResultado>
{
    Task<TResultado> HandleAsync(TCommand command, CancellationToken ct = default);
}

public interface IQuery<TResultado>;

public interface IQueryHandler<in TQuery, TResultado> where TQuery : IQuery<TResultado>
{
    Task<TResultado> HandleAsync(TQuery query, CancellationToken ct = default);
}

/// <summary>Valor vacío para comandos que no devuelven datos.</summary>
public readonly record struct Unit
{
    public static readonly Unit Value = new();
}
