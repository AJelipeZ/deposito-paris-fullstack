using System.Data;
using Microsoft.Data.Sqlite;

namespace DepositoParis.Infrastructure.Persistence;

public interface IDbConnectionFactory
{
    IDbConnection CrearConexion();
}

public class SqliteConnectionFactory(string cadenaConexion) : IDbConnectionFactory
{
    public IDbConnection CrearConexion()
    {
        var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();
        return conexion;
    }
}
