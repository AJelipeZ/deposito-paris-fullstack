using DepositoParis.Application.Abstractions;
using DepositoParis.Application.Dtos;

namespace DepositoParis.Application.Conductores.Queries;

public record ListarConductoresQuery : IQuery<IReadOnlyList<ConductorDto>>;

public class ListarConductoresHandler(IConductorReadRepository repositorio)
    : IQueryHandler<ListarConductoresQuery, IReadOnlyList<ConductorDto>>
{
    public Task<IReadOnlyList<ConductorDto>> HandleAsync(ListarConductoresQuery query, CancellationToken ct = default) =>
        repositorio.ListarAsync();
}
