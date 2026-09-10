using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using WPF_SP.Models;

namespace WPF_SP.Data;

public class CategoriaRepository : ICategoriaRepository
{
    public List<Categoria> ListarCategorias()
    {
        var list = new List<Categoria>();
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        {
            using (var cmd = new SqlCommand("sp_ListarCategorias", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Categoria
                        {
                            CategoriaID = Convert.ToInt32(reader["CategoriaID"]),
                            NombreCategoria = reader["NombreCategoria"].ToString()!,
                            Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : null
                        });
                    }
                }
            }
        }
        return list;
    }

    public void InsertarCategoria(Categoria categoria)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        {
            using (var cmd = new SqlCommand("sp_InsertarCategoria", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)categoria.Descripcion ?? DBNull.Value);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void ActualizarCategoria(Categoria categoria)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        {
            using (var cmd = new SqlCommand("sp_ActualizarCategoria", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoriaID", categoria.CategoriaID);
                cmd.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)categoria.Descripcion ?? DBNull.Value);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void EliminarCategoria(int categoriaID)
    {
        using (var con = new SqlConnection(DbConfig.ConnectionString))
        {
            using (var cmd = new SqlCommand("sp_EliminarCategoria", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
