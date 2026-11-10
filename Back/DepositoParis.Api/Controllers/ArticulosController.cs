using DepositoParis.Application.Abstractions;
using DepositoParis.Application.Articulos.Queries;
using DepositoParis.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DepositoParis.Api.Controllers;

[ApiController]
[Route("api/articulos")]
public class ArticulosController(IDispatcher dispatcher) : ControllerBase
{
    /// <summary>Inventario. Filtros opcionales: búsqueda y soloStockBajo (stock ≤ 10).</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ArticuloDto>>> Listar(
        [FromQuery] string? busqueda, [FromQuery] bool soloStockBajo, CancellationToken ct) =>
        Ok(await dispatcher.QueryAsync(new ListarArticulosQuery(busqueda, soloStockBajo), ct));
}
