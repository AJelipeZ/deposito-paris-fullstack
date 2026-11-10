namespace DepositoParis.Domain.Exceptions;

/// <summary>Una regla del negocio no se cumple (HTTP 409).</summary>
public class ReglaNegocioException(string mensaje) : Exception(mensaje);

/// <summary>El recurso solicitado no existe (HTTP 404).</summary>
public class NoEncontradoException(string mensaje) : Exception(mensaje);

/// <summary>Los datos enviados no son válidos (HTTP 400).</summary>
public class ValidacionException(IDictionary<string, string[]> errores)
    : Exception("Uno o más datos no son válidos.")
{
    public IDictionary<string, string[]> Errores { get; } = errores;
}
