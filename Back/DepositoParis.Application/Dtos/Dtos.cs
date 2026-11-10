namespace DepositoParis.Application.Dtos;

// DTOs de salida de la API (lado de lectura).

public class RemisionResumenDto
{
    public int Id { get; init; }
    public string Cliente { get; init; } = string.Empty;
    public string Telefono { get; init; } = string.Empty;
    public string Fecha { get; init; } = string.Empty;
    public string Estado { get; init; } = string.Empty;
    public string Conductor { get; init; } = string.Empty;
    public int CantidadItems { get; init; }
    public long Total { get; init; }
}

public class ItemRemisionDto
{
    public int ArticuloId { get; init; }
    public string Descripcion { get; init; } = string.Empty;
    public int Cantidad { get; init; }
    public int Precio { get; init; }
    public long Subtotal { get; init; }
}

public class RemisionDetalleDto
{
    public int Id { get; init; }
    public string Cliente { get; init; } = string.Empty;
    public string Telefono { get; init; } = string.Empty;
    public string Fecha { get; init; } = string.Empty;
    public string Estado { get; init; } = string.Empty;
    public int ConductorId { get; init; }
    public string Conductor { get; init; } = string.Empty;
    public string Observaciones { get; init; } = string.Empty;
    public long Total { get; set; }
    public IReadOnlyList<ItemRemisionDto> Items { get; set; } = [];
}

public class ArticuloDto
{
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public int Stock { get; init; }
}

public class ConductorDto
{
    public int Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
}

public class ResumenDto
{
    public int Pendientes { get; init; }
    public int Despachadas { get; init; }
    public int Entregadas { get; init; }
    public int Rechazadas { get; init; }
    public int ArticulosStockBajo { get; init; }
}
