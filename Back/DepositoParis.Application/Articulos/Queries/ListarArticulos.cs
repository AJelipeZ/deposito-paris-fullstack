using DepositoParis.Application.Abstractions;
using DepositoParis.Application.Dtos;

namespace DepositoParis.Application.Articulos.Queries;

public record ListarArticulosQuery(string? Busqueda, bool SoloStockBajo) : IQuery<IReadOnlyList<ArticuloDto>>;

public class ListarArticulosHandler(IArticuloReadRepository repositorio)
    : IQueryHandler<ListarArticulosQuery, IReadOnlyList<ArticuloDto>>
{
    public const int UmbralStockBajo = 10;

    public Task<IReadOnlyList<ArticuloDto>> HandleAsync(ListarArticulosQuery query, CancellationToken ct = default) =>
        repositorio.ListarAsync(query.Busqueda, query.SoloStockBajo ? UmbralStockBajo : null);
}
