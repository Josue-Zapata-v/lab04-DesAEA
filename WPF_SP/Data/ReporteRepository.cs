using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using WPF_SP.Models;

namespace WPF_SP.Data;

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
                    list.Add(new DetallePedidoReporte
                    {
                        PedidoID = Convert.ToInt32(reader["PedidoID"]),
                        FechaPedido = reader["FechaPedido"] != DBNull.Value ? Convert.ToDateTime(reader["FechaPedido"]) : null,
                        Cliente = reader["Cliente"] != DBNull.Value ? reader["Cliente"].ToString() : null,
                        Producto = reader["Producto"] != DBNull.Value ? reader["Producto"].ToString() : null,
                        PrecioUnidad = Convert.ToDecimal(reader["PrecioUnidad"]),
                        Cantidad = Convert.ToInt16(reader["Cantidad"]),
                        Descuento = Convert.ToSingle(reader["Descuento"]),
                        Subtotal = Convert.ToDecimal(reader["Subtotal"])
                    });
                }
            }
        }
        return list;
    }
}
