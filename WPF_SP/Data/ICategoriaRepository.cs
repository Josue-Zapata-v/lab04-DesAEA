using System.Collections.Generic;
using WPF_SP.Models;

namespace WPF_SP.Data;

public interface ICategoriaRepository
{
    List<Categoria> ListarCategorias();
    void InsertarCategoria(Categoria categoria);
    void ActualizarCategoria(Categoria categoria);
    void EliminarCategoria(int categoriaID);
}
