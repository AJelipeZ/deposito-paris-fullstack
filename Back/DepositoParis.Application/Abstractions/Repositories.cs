using DepositoParis.Application.Dtos;
using DepositoParis.Domain.Entities;

namespace DepositoParis.Application.Abstractions;

// Repositorios de LECTURA: devuelven DTOs listos para la API (lado Query).

public interface IRemisionReadRepository
{
    Task<IReadOnlyList<RemisionResumenDto>> ListarAsync(string? estado, string? busqueda);
    Task<RemisionDetalleDto?> ObtenerPorIdAsync(int id);
    Task<ResumenDto> ObtenerResumenAsync(int umbralStockBajo);
}

public interface IArticuloReadRepository
{
    Task<IReadOnlyList<ArticuloDto>> ListarAsync(string? busqueda, int? stockMaximo);
    Task<IReadOnlyList<ArticuloDto>> ObtenerPorIdsAsync(IEnumerable<int> ids);
}

public interface IConductorReadRepository
{
    Task<IReadOnlyList<ConductorDto>> ListarAsync();
    Task<bool> ExisteAsync(int id);
}

// Repositorio de ESCRITURA: trabaja con entidades del dominio (lado Command).

public interface IRemisionWriteRepository
{
    Task<Remision?> ObtenerAsync(int id);
    Task<int> CrearAsync(Remision remision);

    /// <summary>Guarda el nuevo estado; si es "entregado" descuenta el inventario en la misma transacción.</summary>
    Task ActualizarEstadoAsync(Remision remision, bool descontarInventario);
}
