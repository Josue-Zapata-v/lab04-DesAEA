using System.Collections.Generic;
using WPF_SP.Models;

namespace WPF_SP.Data;

public interface IProductoRepository
{
    List<Producto> ListarProductos();
    void InsertarProducto(Producto producto);
    void ActualizarProducto(Producto producto);
    void EliminarProducto(int productoID);
}
