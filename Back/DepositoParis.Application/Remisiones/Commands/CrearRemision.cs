using System.Text.RegularExpressions;
using DepositoParis.Application.Abstractions;
using DepositoParis.Domain.Entities;
using DepositoParis.Domain.Exceptions;

namespace DepositoParis.Application.Remisiones.Commands;

public record CrearItemRequest(int ArticuloId, int Cantidad, int Precio);

public record CrearRemisionCommand(
    string Cliente, string Telefono, int ConductorId, string? Observaciones,
    IReadOnlyList<CrearItemRequest> Items) : ICommand<int>;

public partial class CrearRemisionHandler(
    IRemisionWriteRepository remisiones,
    IConductorReadRepository conductores,
    IArticuloReadRepository articulos) : ICommandHandler<CrearRemisionCommand, int>
{
    [GeneratedRegex(@"^3\d{9}$")]
    private static partial Regex TelefonoColombia();

    public async Task<int> HandleAsync(CrearRemisionCommand command, CancellationToken ct = default)
    {
        var errores = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Cliente) || command.Cliente.Trim().Length < 3)
            errores["cliente"] = ["El cliente es obligatorio y debe tener mínimo 3 caracteres."];

        if (!TelefonoColombia().IsMatch(command.Telefono ?? string.Empty))
            errores["telefono"] = ["El teléfono debe tener 10 dígitos y empezar por 3."];

        if ((command.Observaciones?.Length ?? 0) > 200)
            errores["observaciones"] = ["Las observaciones admiten máximo 200 caracteres."];

        if (command.Items is null || command.Items.Count == 0)
            errores["items"] = ["La remisión debe tener al menos un artículo."];
        else if (command.Items.Any(i => i.Cantidad <= 0 || i.Precio <= 0))
            errores["items"] = ["Cada artículo debe tener cantidad y precio mayores a 0."];
        else if (command.Items.Select(i => i.ArticuloId).Distinct().Count() != command.Items.Count)
            errores["items"] = ["No se puede repetir un artículo en la misma remisión."];

        if (!await conductores.ExisteAsync(command.ConductorId))
            errores["conductorId"] = ["El conductor seleccionado no existe."];

        if (command.Items is { Count: > 0 })
        {
            var existentes = await articulos.ObtenerPorIdsAsync(command.Items.Select(i => i.ArticuloId));
            if (existentes.Count != command.Items.Count)
                errores["items"] = ["Uno o más artículos no existen en el inventario."];
        }

        if (errores.Count > 0)
            throw new ValidacionException(errores);

        var remision = new Remision
        {
            Cliente = command.Cliente.Trim(),
            Telefono = command.Telefono!,
            Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
            ConductorId = command.ConductorId,
            Observaciones = command.Observaciones?.Trim() ?? string.Empty,
            Items = command.Items!
                .Select(i => new ItemRemision { ArticuloId = i.ArticuloId, Cantidad = i.Cantidad, Precio = i.Precio })
                .ToList(),
        };

        return await remisiones.CrearAsync(remision);
    }
}
