using DepositoParis.Application.Abstractions;
using DepositoParis.Application.Dtos;
using DepositoParis.Application.Resumen.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DepositoParis.Api.Controllers;

[ApiController]
[Route("api/resumen")]
public class ResumenController(IDispatcher dispatcher) : ControllerBase
{
    /// <summary>Totales para la pantalla de inicio.</summary>
    [HttpGet]
    public async Task<ActionResult<ResumenDto>> Obtener(CancellationToken ct) =>
        Ok(await dispatcher.QueryAsync(new ObtenerResumenQuery(), ct));
}
