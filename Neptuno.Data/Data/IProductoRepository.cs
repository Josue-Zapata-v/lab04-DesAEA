using System.Collections.Generic;
using System.Threading.Tasks;
using Neptuno.Data.Models;

namespace Neptuno.Data.Data;

public interface IProductoRepository
{
    List<Producto> ListarProductos();
    Task<List<Producto>> ListarProductosAsync();

    void InsertarProducto(Producto producto);
    Task InsertarProductoAsync(Producto producto);

    void ActualizarProducto(Producto producto);
    Task ActualizarProductoAsync(Producto producto);

    void EliminarProducto(int productoID);
    Task EliminarProductoAsync(int productoID);
}
