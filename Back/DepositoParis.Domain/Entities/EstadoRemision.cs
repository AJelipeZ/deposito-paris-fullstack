namespace DepositoParis.Domain.Entities;

/// <summary>Estados posibles de una remisión y sus transiciones válidas.</summary>
public static class EstadoRemision
{
    public const string Pendiente = "pendiente";
    public const string Despachado = "despachado";
    public const string Entregado = "entregado";
    public const string Rechazado = "rechazado";

    public static readonly IReadOnlyList<string> Todos = [Pendiente, Despachado, Entregado, Rechazado];

    private static readonly Dictionary<string, string[]> Transiciones = new()
    {
        [Pendiente] = [Despachado, Rechazado],
        [Despachado] = [Entregado, Rechazado],
        [Entregado] = [],
        [Rechazado] = [],
    };

    public static bool EsValido(string estado) => Transiciones.ContainsKey(estado);

    public static bool PuedeCambiar(string desde, string hacia) =>
        Transiciones.TryGetValue(desde, out var destinos) && destinos.Contains(hacia);
}
