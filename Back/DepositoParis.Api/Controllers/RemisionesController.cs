using DepositoParis.Api.Contracts;
using DepositoParis.Application.Abstractions;
using DepositoParis.Application.Dtos;
using DepositoParis.Application.Remisiones.Commands;
using DepositoParis.Application.Remisiones.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DepositoParis.Api.Controllers;

[ApiController]
[Route("api/remisiones")]
public class RemisionesController(IDispatcher dispatcher) : ControllerBase
{
    /// <summary>Lista las remisiones. Filtros opcionales: estado y búsqueda por cliente o número.</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RemisionResumenDto>>> Listar(
        [FromQuery] string? estado, [FromQuery] string? busqueda, CancellationToken ct) =>
        Ok(await dispatcher.QueryAsync(new ListarRemisionesQuery(estado, busqueda), ct));

    /// <summary>Detalle de una remisión con sus artículos.</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RemisionDetalleDto>> Obtener(int id, CancellationToken ct) =>
        Ok(await dispatcher.QueryAsync(new ObtenerRemisionQuery(id), ct));

    /// <summary>Crea una remisión en estado pendiente.</summary>
    [HttpPost]
    public async Task<ActionResult<RemisionDetalleDto>> Crear(CrearRemisionCommand command, CancellationToken ct)
    {
        var id = await dispatcher.SendAsync(command, ct);
        var creada = await dispatcher.QueryAsync(new ObtenerRemisionQuery(id), ct);
        return CreatedAtAction(nameof(Obtener), new { id }, creada);
    }

    /// <summary>Cambia el estado. Al pasar a "entregado" se descuenta el inventario.</summary>
    [HttpPatch("{id:int}/estado")]
    public async Task<ActionResult<RemisionDetalleDto>> CambiarEstado(
        int id, CambiarEstadoRequest request, CancellationToken ct)
    {
        await dispatcher.SendAsync(new CambiarEstadoRemisionCommand(id, request.Estado), ct);
        return Ok(await dispatcher.QueryAsync(new ObtenerRemisionQuery(id), ct));
    }
}
