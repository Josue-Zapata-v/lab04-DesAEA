using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Neptuno.Data.Models;

namespace Neptuno.Data.Data;

public class PedidoRepository : IPedidoRepository
{
    public List<Pedido> ListarPedidos()
    {
        var list = new List<Pedido>();
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_ListarPedidos", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(MapPedido(reader));
                }
            }
        }
        return list;
    }

    public async Task<List<Pedido>> ListarPedidosAsync()
    {
        var list = new List<Pedido>();
        await using (var con = new SqlConnection(DbConfig.ConnectionString))
        await using (var cmd = new SqlCommand("sp_ListarPedidos", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            await con.OpenAsync();
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    list.Add(MapPedido(reader));
                }
            }
        }
        return list;
    }

    /// <summary>
    /// REQUERIMIENTO 14 - MODO DESCONECTADO
    /// Carga el conjunto de pedidos en un DataSet desconectado utilizando SqlDataAdapter.
    /// </summary>
    public DataSet ListarPedidosDesconectado()
    {
        var ds = new DataSet("NeptunoPedidos");
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_ListarPedidos", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            using (var adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(ds, "Pedidos");
            }
        }
        return ds;
    }

    public Task<DataSet> ListarPedidosDesconectadoAsync()
    {
        return Task.Run(() => ListarPedidosDesconectado());
    }

    public void InsertarPedido(Pedido pedido)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_InsertarPedido", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            CargarParametrosPedido(cmd, pedido);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public async Task InsertarPedidoAsync(Pedido pedido)
    {
        await using (var con = new SqlConnection(DbConfig.ConnectionString))
        await using (var cmd = new SqlCommand("sp_InsertarPedido", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            CargarParametrosPedido(cmd, pedido);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }

    public void ActualizarPedido(Pedido pedido)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_ActualizarPedido", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PedidoID", pedido.PedidoID);
            CargarParametrosPedido(cmd, pedido);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public async Task ActualizarPedidoAsync(Pedido pedido)
    {
        await using (var con = new SqlConnection(DbConfig.ConnectionString))
        await using (var cmd = new SqlCommand("sp_ActualizarPedido", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PedidoID", pedido.PedidoID);
            CargarParametrosPedido(cmd, pedido);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }

    public void EliminarPedido(int pedidoID)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_EliminarPedido", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PedidoID", pedidoID);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public async Task EliminarPedidoAsync(int pedidoID)
    {
        await using (var con = new SqlConnection(DbConfig.ConnectionString))
        await using (var cmd = new SqlCommand("sp_EliminarPedido", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PedidoID", pedidoID);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }

    private static void CargarParametrosPedido(SqlCommand cmd, Pedido pedido)
    {
        cmd.Parameters.AddWithValue("@ClienteID", (object?)pedido.ClienteID ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@EmpleadoID", (object?)pedido.EmpleadoID ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@FechaPedido", (object?)pedido.FechaPedido ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@FechaRequerida", (object?)pedido.FechaRequerida ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@FechaEnvio", (object?)pedido.FechaEnvio ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TransportistaID", (object?)pedido.TransportistaID ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Destinatario", (object?)pedido.Destinatario ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@CiudadDestino", (object?)pedido.CiudadDestino ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@PaisDestino", (object?)pedido.PaisDestino ?? DBNull.Value);
    }

    private static Pedido MapPedido(SqlDataReader reader)
    {
        return new Pedido
        {
            PedidoID = Convert.ToInt32(reader["PedidoID"]),
            ClienteID = reader["ClienteID"] != DBNull.Value ? reader["ClienteID"].ToString() : null,
            Cliente = reader["Cliente"] != DBNull.Value ? reader["Cliente"].ToString() : null,
            EmpleadoID = reader["EmpleadoID"] != DBNull.Value ? Convert.ToInt32(reader["EmpleadoID"]) : null,
            Empleado = reader["Empleado"] != DBNull.Value ? reader["Empleado"].ToString() : null,
            FechaPedido = reader["FechaPedido"] != DBNull.Value ? Convert.ToDateTime(reader["FechaPedido"]) : null,
            FechaRequerida = reader["FechaRequerida"] != DBNull.Value ? Convert.ToDateTime(reader["FechaRequerida"]) : null,
            FechaEnvio = reader["FechaEnvio"] != DBNull.Value ? Convert.ToDateTime(reader["FechaEnvio"]) : null,
            TransportistaID = reader["TransportistaID"] != DBNull.Value ? Convert.ToInt32(reader["TransportistaID"]) : null,
            Transportista = reader["Transportista"] != DBNull.Value ? reader["Transportista"].ToString() : null,
            Destinatario = reader["Destinatario"] != DBNull.Value ? reader["Destinatario"].ToString() : null,
            CiudadDestino = reader["CiudadDestino"] != DBNull.Value ? reader["CiudadDestino"].ToString() : null,
            PaisDestino = reader["PaisDestino"] != DBNull.Value ? reader["PaisDestino"].ToString() : null
        };
    }
}
