using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Neptuno.Data.Models;

namespace Neptuno.Data.Data;

public interface IProveedorRepository
{
    List<Proveedor> ListarProveedores();
    Task<List<Proveedor>> ListarProveedoresAsync();

    void InsertarProveedor(Proveedor proveedor);
    Task InsertarProveedorAsync(Proveedor proveedor);

    void ActualizarProveedor(Proveedor proveedor);
    Task ActualizarProveedorAsync(Proveedor proveedor);

    void EliminarProveedor(int proveedorID);
    Task EliminarProveedorAsync(int proveedorID);

    List<Proveedor> BuscarProveedores(string nombreContacto, string ciudad);
    Task<List<Proveedor>> BuscarProveedoresAsync(string nombreContacto, string ciudad);

    // MODO DESCONECTADO (SqlDataAdapter y DataTable)
    DataTable BuscarProveedoresDesconectado(string nombreContacto, string ciudad);
    Task<DataTable> BuscarProveedoresDesconectadoAsync(string nombreContacto, string ciudad);
}
