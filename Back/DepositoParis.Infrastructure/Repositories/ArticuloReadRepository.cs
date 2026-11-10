using Dapper;
using DepositoParis.Application.Abstractions;
using DepositoParis.Application.Dtos;
using DepositoParis.Infrastructure.Persistence;

namespace DepositoParis.Infrastructure.Repositories;

public class ArticuloReadRepository(IDbConnectionFactory factory) : IArticuloReadRepository
{
    public async Task<IReadOnlyList<ArticuloDto>> ListarAsync(string? busqueda, int? stockMaximo)
    {
        const string sql = """
            SELECT id AS Id, nombre AS Nombre, stock AS Stock
            FROM articulos
            WHERE (@Busqueda IS NULL
                   OR lower(nombre) LIKE '%' || lower(@Busqueda) || '%'
                   OR CAST(id AS TEXT) LIKE '%' || @Busqueda || '%')
              AND (@StockMaximo IS NULL OR stock <= @StockMaximo)
            ORDER BY nombre
            """;

        using var conexion = factory.CrearConexion();
        var filas = await conexion.QueryAsync<ArticuloDto>(sql, new
        {
            Busqueda = string.IsNullOrWhiteSpace(busqueda) ? null : busqueda.Trim(),
            StockMaximo = stockMaximo,
        });
        return filas.ToList();
    }

    public async Task<IReadOnlyList<ArticuloDto>> ObtenerPorIdsAsync(IEnumerable<int> ids)
    {
        using var conexion = factory.CrearConexion();
        var filas = await conexion.QueryAsync<ArticuloDto>(
            "SELECT id AS Id, nombre AS Nombre, stock AS Stock FROM articulos WHERE id IN @Ids",
            new { Ids = ids.ToArray() });
        return filas.ToList();
    }
}
