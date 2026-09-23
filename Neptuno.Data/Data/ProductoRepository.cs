using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Neptuno.Data.Models;

namespace Neptuno.Data.Data;

public class ProductoRepository : IProductoRepository
{
    public List<Producto> ListarProductos()
    {
        var list = new List<Producto>();
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_ListarProductos", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(MapProducto(reader));
                }
            }
        }
        return list;
    }

    public async Task<List<Producto>> ListarProductosAsync()
    {
        var list = new List<Producto>();
        await using (var con = new SqlConnection(DbConfig.ConnectionString))
        await using (var cmd = new SqlCommand("sp_ListarProductos", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            await con.OpenAsync();
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    list.Add(MapProducto(reader));
                }
            }
        }
        return list;
    }

    public void InsertarProducto(Producto producto)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_InsertarProducto", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            CargarParametrosProducto(cmd, producto);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public async Task InsertarProductoAsync(Producto producto)
    {
        await using (var con = new SqlConnection(DbConfig.ConnectionString))
        await using (var cmd = new SqlCommand("sp_InsertarProducto", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            CargarParametrosProducto(cmd, producto);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }

    public void ActualizarProducto(Producto producto)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_ActualizarProducto", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductoID", producto.ProductoID);
            CargarParametrosProducto(cmd, producto);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public async Task ActualizarProductoAsync(Producto producto)
    {
        await using (var con = new SqlConnection(DbConfig.ConnectionString))
        await using (var cmd = new SqlCommand("sp_ActualizarProducto", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductoID", producto.ProductoID);
            CargarParametrosProducto(cmd, producto);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }

    public void EliminarProducto(int productoID)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_EliminarProducto", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductoID", productoID);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public async Task EliminarProductoAsync(int productoID)
    {
        await using (var con = new SqlConnection(DbConfig.ConnectionString))
        await using (var cmd = new SqlCommand("sp_EliminarProducto", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductoID", productoID);
            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }

    private static void CargarParametrosProducto(SqlCommand cmd, Producto producto)
    {
        cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
        cmd.Parameters.AddWithValue("@ProveedorID", (object?)producto.ProveedorID ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@CategoriaID", (object?)producto.CategoriaID ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@CantidadPorUnidad", (object?)producto.CantidadPorUnidad ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@PrecioUnidad", (object?)producto.PrecioUnidad ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@UnidadesEnExistencia", (object?)producto.UnidadesEnExistencia ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@UnidadesEnPedido", (object?)producto.UnidadesEnPedido ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NivelDeReorden", (object?)producto.NivelDeReorden ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Descontinuado", producto.Descontinuado);
    }

    private static Producto MapProducto(SqlDataReader reader)
    {
        return new Producto
        {
            ProductoID = Convert.ToInt32(reader["ProductoID"]),
            NombreProducto = reader["NombreProducto"].ToString()!,
            ProveedorID = reader["ProveedorID"] != DBNull.Value ? Convert.ToInt32(reader["ProveedorID"]) : null,
            Proveedor = reader["Proveedor"] != DBNull.Value ? reader["Proveedor"].ToString() : null,
            CategoriaID = reader["CategoriaID"] != DBNull.Value ? Convert.ToInt32(reader["CategoriaID"]) : null,
            Categoria = reader["Categoria"] != DBNull.Value ? reader["Categoria"].ToString() : null,
            CantidadPorUnidad = reader["CantidadPorUnidad"] != DBNull.Value ? reader["CantidadPorUnidad"].ToString() : null,
            PrecioUnidad = reader["PrecioUnidad"] != DBNull.Value ? Convert.ToDecimal(reader["PrecioUnidad"]) : null,
            UnidadesEnExistencia = reader["UnidadesEnExistencia"] != DBNull.Value ? Convert.ToInt16(reader["UnidadesEnExistencia"]) : null,
            UnidadesEnPedido = reader["UnidadesEnPedido"] != DBNull.Value ? Convert.ToInt16(reader["UnidadesEnPedido"]) : null,
            NivelDeReorden = reader["NivelDeReorden"] != DBNull.Value ? Convert.ToInt16(reader["NivelDeReorden"]) : null,
            Descontinuado = Convert.ToBoolean(reader["Descontinuado"])
        };
    }
}
