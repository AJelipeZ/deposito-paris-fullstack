using System.Reflection;
using Dapper;
using Microsoft.Extensions.Logging;

namespace DepositoParis.Infrastructure.Persistence;

/// <summary>Crea las tablas y carga los datos de prueba la primera vez que se ejecuta la API.</summary>
public class DatabaseInitializer(IDbConnectionFactory factory, ILogger<DatabaseInitializer> logger)
{
    public void Inicializar()
    {
        using var conexion = factory.CrearConexion();
        conexion.Execute(LeerScript("schema.sql"));

        var hayDatos = conexion.ExecuteScalar<long>("SELECT COUNT(*) FROM conductores") > 0;
        if (!hayDatos)
        {
            conexion.Execute(LeerScript("seed.sql"));
            logger.LogInformation("Base de datos creada con datos iniciales.");
        }
    }

    private static string LeerScript(string nombre)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var recurso = assembly.GetManifestResourceNames().Single(r => r.EndsWith(nombre));
        using var stream = assembly.GetManifestResourceStream(recurso)!;
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
