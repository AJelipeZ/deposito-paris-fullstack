using DepositoParis.Application.Abstractions;
using DepositoParis.Application.Conductores.Queries;
using DepositoParis.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DepositoParis.Api.Controllers;

[ApiController]
[Route("api/conductores")]
public class ConductoresController(IDispatcher dispatcher) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ConductorDto>>> Listar(CancellationToken ct) =>
        Ok(await dispatcher.QueryAsync(new ListarConductoresQuery(), ct));
}
