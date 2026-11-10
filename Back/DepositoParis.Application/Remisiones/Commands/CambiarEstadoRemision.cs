using DepositoParis.Application.Abstractions;
using DepositoParis.Domain.Entities;
using DepositoParis.Domain.Exceptions;

namespace DepositoParis.Application.Remisiones.Commands;

public record CambiarEstadoRemisionCommand(int Id, string Estado) : ICommand<Unit>;

public class CambiarEstadoRemisionHandler(IRemisionWriteRepository remisiones)
    : ICommandHandler<CambiarEstadoRemisionCommand, Unit>
{
    public async Task<Unit> HandleAsync(CambiarEstadoRemisionCommand command, CancellationToken ct = default)
    {
        var remision = await remisiones.ObtenerAsync(command.Id)
            ?? throw new NoEncontradoException($"La remisión N° {command.Id} no existe.");

        // La entidad valida la transición (pendiente -> despachado -> entregado / rechazado)
        remision.CambiarEstado(command.Estado?.Trim().ToLowerInvariant() ?? string.Empty);

        var descontar = remision.Estado == EstadoRemision.Entregado;
        await remisiones.ActualizarEstadoAsync(remision, descontar);
        return Unit.Value;
    }
}
