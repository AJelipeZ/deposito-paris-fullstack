using DepositoParis.Domain.Exceptions;

namespace DepositoParis.Domain.Entities;

public class Remision
{
    public int Id { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Fecha { get; set; } = string.Empty;
    public string Estado { get; set; } = EstadoRemision.Pendiente;
    public int ConductorId { get; set; }
    public string Observaciones { get; set; } = string.Empty;
    public List<ItemRemision> Items { get; set; } = [];

    /// <summary>Valida y aplica el cambio de estado según las reglas del negocio.</summary>
    public void CambiarEstado(string nuevoEstado)
    {
        if (!EstadoRemision.EsValido(nuevoEstado))
        {
            throw new ReglaNegocioException($"El estado '{nuevoEstado}' no existe.");
        }

        if (!EstadoRemision.PuedeCambiar(Estado, nuevoEstado))
        {
            throw new ReglaNegocioException($"No se puede pasar de '{Estado}' a '{nuevoEstado}'.");
        }

        Estado = nuevoEstado;
    }
}
