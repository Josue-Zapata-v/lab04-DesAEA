using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using WPF_SP.Models;

namespace WPF_SP.Data;

public class ProveedorRepository : IProveedorRepository
{
    public List<Proveedor> ListarProveedores()
    {
        var list = new List<Proveedor>();
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_ListarProveedores", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(MapProveedor(reader));
                }
            }
        }
        return list;
    }

    public List<Proveedor> BuscarProveedores(string nombreContacto, string ciudad)
    {
        var list = new List<Proveedor>();
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_BuscarProveedores", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NombreContacto", string.IsNullOrEmpty(nombreContacto) ? DBNull.Value : nombreContacto);
            cmd.Parameters.AddWithValue("@Ciudad", string.IsNullOrEmpty(ciudad) ? DBNull.Value : ciudad);
            con.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(MapProveedor(reader));
                }
            }
        }
        return list;
    }

    public void InsertarProveedor(Proveedor proveedor)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_InsertarProveedor", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompaniaNombre", proveedor.CompaniaNombre);
            cmd.Parameters.AddWithValue("@NombreContacto", (object?)proveedor.NombreContacto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CargoContacto", (object?)proveedor.CargoContacto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Direccion", (object?)proveedor.Direccion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Ciudad", (object?)proveedor.Ciudad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CodigoPostal", (object?)proveedor.CodigoPostal ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Pais", (object?)proveedor.Pais ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Telefono", (object?)proveedor.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Fax", (object?)proveedor.Fax ?? DBNull.Value);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void ActualizarProveedor(Proveedor proveedor)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_ActualizarProveedor", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProveedorID", proveedor.ProveedorID);
            cmd.Parameters.AddWithValue("@CompaniaNombre", proveedor.CompaniaNombre);
            cmd.Parameters.AddWithValue("@NombreContacto", (object?)proveedor.NombreContacto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CargoContacto", (object?)proveedor.CargoContacto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Direccion", (object?)proveedor.Direccion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Ciudad", (object?)proveedor.Ciudad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CodigoPostal", (object?)proveedor.CodigoPostal ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Pais", (object?)proveedor.Pais ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Telefono", (object?)proveedor.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Fax", (object?)proveedor.Fax ?? DBNull.Value);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void EliminarProveedor(int proveedorID)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        using (var cmd = new SqlCommand("sp_EliminarProveedor", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProveedorID", proveedorID);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    private Proveedor MapProveedor(SqlDataReader reader)
    {
        return new Proveedor
        {
            ProveedorID = Convert.ToInt32(reader["ProveedorID"]),
            CompaniaNombre = reader["CompaniaNombre"].ToString()!,
            NombreContacto = reader["NombreContacto"] != DBNull.Value ? reader["NombreContacto"].ToString() : null,
            CargoContacto = reader["CargoContacto"] != DBNull.Value ? reader["CargoContacto"].ToString() : null,
            Direccion = reader["Direccion"] != DBNull.Value ? reader["Direccion"].ToString() : null,
            Ciudad = reader["Ciudad"] != DBNull.Value ? reader["Ciudad"].ToString() : null,
            CodigoPostal = reader["CodigoPostal"] != DBNull.Value ? reader["CodigoPostal"].ToString() : null,
            Pais = reader["Pais"] != DBNull.Value ? reader["Pais"].ToString() : null,
            Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : null,
            Fax = reader["Fax"] != DBNull.Value ? reader["Fax"].ToString() : null
        };
    }
}
