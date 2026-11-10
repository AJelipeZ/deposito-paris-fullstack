using Dapper;
using DepositoParis.Application.Abstractions;
using DepositoParis.Application.Dtos;
using DepositoParis.Domain.Entities;
using DepositoParis.Infrastructure.Persistence;

namespace DepositoParis.Infrastructure.Repositories;

public class RemisionReadRepository(IDbConnectionFactory factory) : IRemisionReadRepository
{
    public async Task<IReadOnlyList<RemisionResumenDto>> ListarAsync(string? estado, string? busqueda)
    {
        const string sql = """
            SELECT r.id AS Id, r.cliente AS Cliente, r.telefono AS Telefono, r.fecha AS Fecha,
                   r.estado AS Estado, c.nombre AS Conductor,
                   COUNT(i.id) AS CantidadItems,
                   COALESCE(SUM(i.cantidad * i.precio), 0) AS Total
            FROM remisiones r
            JOIN conductores c ON c.id = r.conductor_id
            LEFT JOIN remision_items i ON i.remision_id = r.id
            WHERE (@Estado IS NULL OR r.estado = @Estado)
              AND (@Busqueda IS NULL
                   OR lower(r.cliente) LIKE '%' || lower(@Busqueda) || '%'
                   OR CAST(r.id AS TEXT) LIKE '%' || @Busqueda || '%')
            GROUP BY r.id
            ORDER BY r.id DESC
            """;

        using var conexion = factory.CrearConexion();
        var filas = await conexion.QueryAsync<RemisionResumenDto>(sql, new
        {
            Estado = string.IsNullOrWhiteSpace(estado) ? null : estado,
            Busqueda = string.IsNullOrWhiteSpace(busqueda) ? null : busqueda.Trim(),
        });
        return filas.ToList();
    }

    public async Task<RemisionDetalleDto?> ObtenerPorIdAsync(int id)
    {
        const string sqlRemision = """
            SELECT r.id AS Id, r.cliente AS Cliente, r.telefono AS Telefono, r.fecha AS Fecha,
                   r.estado AS Estado, r.conductor_id AS ConductorId, c.nombre AS Conductor,
                   r.observaciones AS Observaciones
            FROM remisiones r
            JOIN conductores c ON c.id = r.conductor_id
            WHERE r.id = @id
            """;
        const string sqlItems = """
            SELECT i.articulo_id AS ArticuloId, a.nombre AS Descripcion, i.cantidad AS Cantidad,
                   i.precio AS Precio, (i.cantidad * i.precio) AS Subtotal
            FROM remision_items i
            JOIN articulos a ON a.id = i.articulo_id
            WHERE i.remision_id = @id
            ORDER BY i.id
            """;

        using var conexion = factory.CrearConexion();
        var remision = await conexion.QuerySingleOrDefaultAsync<RemisionDetalleDto>(sqlRemision, new { id });
        if (remision is null)
        {
            return null;
        }

        var items = (await conexion.QueryAsync<ItemRemisionDto>(sqlItems, new { id })).ToList();
        remision.Items = items;
        remision.Total = items.Sum(i => i.Subtotal);
        return remision;
    }

    public async Task<ResumenDto> ObtenerResumenAsync(int umbralStockBajo)
    {
        const string sql = """
            SELECT
              (SELECT COUNT(*) FROM remisiones WHERE estado = @Pendiente)  AS Pendientes,
              (SELECT COUNT(*) FROM remisiones WHERE estado = @Despachado) AS Despachadas,
              (SELECT COUNT(*) FROM remisiones WHERE estado = @Entregado)  AS Entregadas,
              (SELECT COUNT(*) FROM remisiones WHERE estado = @Rechazado)  AS Rechazadas,
              (SELECT COUNT(*) FROM articulos WHERE stock <= @Umbral)      AS ArticulosStockBajo
            """;

        using var conexion = factory.CrearConexion();
        return await conexion.QuerySingleAsync<ResumenDto>(sql, new
        {
            EstadoRemision.Pendiente,
            EstadoRemision.Despachado,
            EstadoRemision.Entregado,
            EstadoRemision.Rechazado,
            Umbral = umbralStockBajo,
        });
    }
}
