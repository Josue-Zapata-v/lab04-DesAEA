using System;

namespace WPF_SP.Models;

public class DetallePedidoReporte
{
    public int PedidoID { get; set; }
    public DateTime? FechaPedido { get; set; }
    public string? Cliente { get; set; }
    public string? Producto { get; set; }
    public decimal PrecioUnidad { get; set; }
    public short Cantidad { get; set; }
    public float Descuento { get; set; }
    public decimal Subtotal { get; set; }
}
