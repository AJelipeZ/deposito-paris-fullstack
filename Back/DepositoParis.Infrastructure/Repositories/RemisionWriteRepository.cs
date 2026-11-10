using Dapper;
using DepositoParis.Application.Abstractions;
using DepositoParis.Domain.Entities;
using DepositoParis.Domain.Exceptions;
using DepositoParis.Infrastructure.Persistence;

namespace DepositoParis.Infrastructure.Repositories;

public class RemisionWriteRepository(IDbConnectionFactory factory) : IRemisionWriteRepository
{
    public async Task<Remision?> ObtenerAsync(int id)
    {
        using var conexion = factory.CrearConexion();
        var remision = await conexion.QuerySingleOrDefaultAsync<Remision>("""
            SELECT id AS Id, cliente AS Cliente, telefono AS Telefono, fecha AS Fecha, estado AS Estado,
                   conductor_id AS ConductorId, observaciones AS Observaciones
            FROM remisiones WHERE id = @id
            """, new { id });

        if (remision is not null)
        {
            remision.Items = (await conexion.QueryAsync<ItemRemision>("""
                SELECT articulo_id AS ArticuloId, cantidad AS Cantidad, precio AS Precio
                FROM remision_items WHERE remision_id = @id
                """, new { id })).ToList();
        }

        return remision;
    }

    public async Task<int> CrearAsync(Remision remision)
    {
        using var conexion = factory.CrearConexion();
        using var transaccion = conexion.BeginTransaction();

        var id = await conexion.ExecuteScalarAsync<int>(
            "SELECT COALESCE(MAX(id), 23300) + 1 FROM remisiones", transaction: transaccion);

        await conexion.ExecuteAsync("""
            INSERT INTO remisiones (id, cliente, telefono, fecha, estado, conductor_id, observaciones)
            VALUES (@Id, @Cliente, @Telefono, @Fecha, @Estado, @ConductorId, @Observaciones)
            """, new { Id = id, remision.Cliente, remision.Telefono, remision.Fecha,
                       remision.Estado, remision.ConductorId, remision.Observaciones }, transaccion);

        await conexion.ExecuteAsync("""
            INSERT INTO remision_items (remision_id, articulo_id, cantidad, precio)
            VALUES (@RemisionId, @ArticuloId, @Cantidad, @Precio)
            """, remision.Items.Select(i => new { RemisionId = id, i.ArticuloId, i.Cantidad, i.Precio }), transaccion);

        transaccion.Commit();
        return id;
    }

    public async Task ActualizarEstadoAsync(Remision remision, bool descontarInventario)
    {
        using var conexion = factory.CrearConexion();
        using var transaccion = conexion.BeginTransaction();

        if (descontarInventario)
        {
            foreach (var item in remision.Items)
            {
                var filas = await conexion.ExecuteAsync("""
                    UPDATE articulos SET stock = stock - @Cantidad
                    WHERE id = @ArticuloId AND stock >= @Cantidad
                    """, new { item.Cantidad, item.ArticuloId }, transaccion);

                if (filas == 0)
                {
                    // Al salir sin Commit la transacción se revierte completa
                    throw new ReglaNegocioException(
                        $"No hay stock suficiente del artículo {item.ArticuloId} para entregar la remisión.");
                }
            }
        }

        await conexion.ExecuteAsync("""
            UPDATE remisiones SET estado = @Estado, actualizada_en = datetime('now', 'localtime')
            WHERE id = @Id
            """, new { remision.Estado, remision.Id }, transaccion);

        transaccion.Commit();
    }
}
