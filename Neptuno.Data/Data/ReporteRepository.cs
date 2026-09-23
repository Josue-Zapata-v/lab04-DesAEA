using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Neptuno.Data.Models;

namespace Neptuno.Data.Data;

public class ReporteRepository : IReporteRepository
{
    public List<DetallePedidoReporte> ReporteDetallePedidosPorFechas(DateTime fechaInicio, DateTime fechaFin)
    {
        var list = new List<DetallePedidoReporte>();
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_ReporteDetallePedidosPorFechas", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
            con.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(MapDetallePedidoReporte(reader));
                }
            }
        }
        return list;
    }

    public async Task<List<DetallePedidoReporte>> ReporteDetallePedidosPorFechasAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var list = new List<DetallePedidoReporte>();
        await using (var con = new SqlConnection(DbConfig.ConnectionString))
        await using (var cmd = new SqlCommand("sp_ReporteDetallePedidosPorFechas", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            cmd.Parameters.AddWithValue("@FechaFin", fechaFin);
            await con.OpenAsync();
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    list.Add(MapDetallePedidoReporte(reader));
                }
            }
        }
        return list;
    }

    /// <summary>
    /// REQUERIMIENTO 14 - MODO DESCONECTADO
    /// Genera el reporte en un DataTable desconectado utilizando SqlDataAdapter.
    /// </summary>
    public DataTable ReporteDetallePedidosPorFechasDesconectado(DateTime fechaInicio, DateTime fechaFin)
    {
        var dt = new DataTable("ReporteDetallePedidos");
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_ReporteDetallePedidosPorFechas", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
            cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

            using (var adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }
        }
        return dt;
    }

    public Task<DataTable> ReporteDetallePedidosPorFechasDesconectadoAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        return Task.Run(() => ReporteDetallePedidosPorFechasDesconectado(fechaInicio, fechaFin));
    }

    private static DetallePedidoReporte MapDetallePedidoReporte(SqlDataReader reader)
    {
        return new DetallePedidoReporte
        {
            PedidoID = Convert.ToInt32(reader["PedidoID"]),
            FechaPedido = reader["FechaPedido"] != DBNull.Value ? Convert.ToDateTime(reader["FechaPedido"]) : null,
            Cliente = reader["Cliente"] != DBNull.Value ? reader["Cliente"].ToString() : null,
            Producto = reader["Producto"] != DBNull.Value ? reader["Producto"].ToString() : null,
            PrecioUnidad = Convert.ToDecimal(reader["PrecioUnidad"]),
            Cantidad = Convert.ToInt16(reader["Cantidad"]),
            Descuento = Convert.ToSingle(reader["Descuento"]),
            Subtotal = Convert.ToDecimal(reader["Subtotal"])
        };
    }
}
