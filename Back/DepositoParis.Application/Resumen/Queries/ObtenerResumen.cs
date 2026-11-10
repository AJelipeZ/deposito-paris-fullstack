using DepositoParis.Application.Abstractions;
using DepositoParis.Application.Articulos.Queries;
using DepositoParis.Application.Dtos;

namespace DepositoParis.Application.Resumen.Queries;

public record ObtenerResumenQuery : IQuery<ResumenDto>;

public class ObtenerResumenHandler(IRemisionReadRepository repositorio) : IQueryHandler<ObtenerResumenQuery, ResumenDto>
{
    public Task<ResumenDto> HandleAsync(ObtenerResumenQuery query, CancellationToken ct = default) =>
        repositorio.ObtenerResumenAsync(ListarArticulosHandler.UmbralStockBajo);
}
