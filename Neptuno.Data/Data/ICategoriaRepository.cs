using System.Collections.Generic;
using System.Threading.Tasks;
using Neptuno.Data.Models;

namespace Neptuno.Data.Data;

public interface ICategoriaRepository
{
    List<Categoria> ListarCategorias();
    Task<List<Categoria>> ListarCategoriasAsync();

    void InsertarCategoria(Categoria categoria);
    Task InsertarCategoriaAsync(Categoria categoria);

    void ActualizarCategoria(Categoria categoria);
    Task ActualizarCategoriaAsync(Categoria categoria);

    void EliminarCategoria(int categoriaID);
    Task EliminarCategoriaAsync(int categoriaID);
}
