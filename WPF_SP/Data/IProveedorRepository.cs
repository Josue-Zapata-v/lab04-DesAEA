using System.Collections.Generic;
using WPF_SP.Models;

namespace WPF_SP.Data;

public interface IProveedorRepository
{
    List<Proveedor> ListarProveedores();
    void InsertarProveedor(Proveedor proveedor);
    void ActualizarProveedor(Proveedor proveedor);
    void EliminarProveedor(int proveedorID);
    List<Proveedor> BuscarProveedores(string nombreContacto, string ciudad);
}
