using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Neptuno.Data.Models;

namespace Neptuno.Data.Data;

public interface IPedidoRepository
{
    List<Pedido> ListarPedidos();
    Task<List<Pedido>> ListarPedidosAsync();

    // MODO DESCONECTADO (SqlDataAdapter y DataSet)
    DataSet ListarPedidosDesconectado();
    Task<DataSet> ListarPedidosDesconectadoAsync();

    void InsertarPedido(Pedido pedido);
    Task InsertarPedidoAsync(Pedido pedido);

    void ActualizarPedido(Pedido pedido);
    Task ActualizarPedidoAsync(Pedido pedido);

    void EliminarPedido(int pedidoID);
    Task EliminarPedidoAsync(int pedidoID);
}
