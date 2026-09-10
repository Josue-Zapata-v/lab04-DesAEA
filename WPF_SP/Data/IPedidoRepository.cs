using System.Collections.Generic;
using WPF_SP.Models;

namespace WPF_SP.Data;

public interface IPedidoRepository
{
    List<Pedido> ListarPedidos();
    void InsertarPedido(Pedido pedido);
    void ActualizarPedido(Pedido pedido);
    void EliminarPedido(int pedidoID);
}
