namespace DepositoParis.Application.Abstractions;

/// <summary>Envía cada comando o consulta a su manejador registrado.</summary>
public interface IDispatcher
{
    Task<TResultado> SendAsync<TResultado>(ICommand<TResultado> command, CancellationToken ct = default);
    Task<TResultado> QueryAsync<TResultado>(IQuery<TResultado> query, CancellationToken ct = default);
}
