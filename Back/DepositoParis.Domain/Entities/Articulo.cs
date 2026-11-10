namespace DepositoParis.Domain.Entities;

public class Articulo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Stock { get; set; }
}
