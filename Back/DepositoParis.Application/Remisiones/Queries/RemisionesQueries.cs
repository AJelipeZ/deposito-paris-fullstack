using DepositoParis.Application.Abstractions;
using DepositoParis.Application.Dtos;
using DepositoParis.Domain.Exceptions;

namespace DepositoParis.Application.Remisiones.Queries;

public record ListarRemisionesQuery(string? Estado, string? Busqueda) : IQuery<IReadOnlyList<RemisionResumenDto>>;

public class ListarRemisionesHandler(IRemisionReadRepository repositorio)
    : IQueryHandler<ListarRemisionesQuery, IReadOnlyList<RemisionResumenDto>>
{
    public Task<IReadOnlyList<RemisionResumenDto>> HandleAsync(ListarRemisionesQuery query, CancellationToken ct = default) =>
        repositorio.ListarAsync(query.Estado, query.Busqueda);
}

public record ObtenerRemisionQuery(int Id) : IQuery<RemisionDetalleDto>;

public class ObtenerRemisionHandler(IRemisionReadRepository repositorio)
    : IQueryHandler<ObtenerRemisionQuery, RemisionDetalleDto>
{
    public async Task<RemisionDetalleDto> HandleAsync(ObtenerRemisionQuery query, CancellationToken ct = default) =>
        await repositorio.ObtenerPorIdAsync(query.Id)
        ?? throw new NoEncontradoException($"La remisión N° {query.Id} no existe.");
}
