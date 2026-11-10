using Dapper;
using DepositoParis.Application.Abstractions;
using DepositoParis.Application.Dtos;
using DepositoParis.Infrastructure.Persistence;

namespace DepositoParis.Infrastructure.Repositories;

public class ConductorReadRepository(IDbConnectionFactory factory) : IConductorReadRepository
{
    public async Task<IReadOnlyList<ConductorDto>> ListarAsync()
    {
        using var conexion = factory.CrearConexion();
        var filas = await conexion.QueryAsync<ConductorDto>(
            "SELECT id AS Id, nombre AS Nombre FROM conductores ORDER BY nombre");
        return filas.ToList();
    }

    public async Task<bool> ExisteAsync(int id)
    {
        using var conexion = factory.CrearConexion();
        return await conexion.ExecuteScalarAsync<long>(
            "SELECT COUNT(*) FROM conductores WHERE id = @id", new { id }) > 0;
    }
}
