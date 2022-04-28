using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InmoNovara.Models
{
    public class RepositorioUsuario
    {
        String ConnectionString = "Server=localhost;User=root;Password=;Database=InmoNovara;SslMode=none";

        public RepositorioUsuario()
        {

        }

        public IList<Usuario> ObtenerUsuario()
        {
            var res = new List<Usuario>();
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"Select IdUsuario,Nombre,Apellido,Email,Rol From Usuario;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while(reader.Read())
                    {
                        var p = new Usuario
                        {
                            IdUsuario = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Email = reader.GetString(3),
                            Rol = reader.GetInt32(4),
                        };
                        res.Add(p);
                    }
                    conn.Close();
                }
            }
            return res;
        }

        public int Alta(Usuario u)
        {
            var res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"insert into Usuario (Nombre,Apellido,Email,Clave,Rol) 
                            Values (@{nameof(u.Nombre)},@{nameof(u.Apellido)},@{nameof(u.Email)},@{nameof(u.Clave)},@{nameof(u.Rol)});
                            Select last_Insert_Id();";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue($"@{nameof(u.Nombre)}",u.Nombre);
                    comm.Parameters.AddWithValue($"@{nameof(u.Apellido)}",u.Apellido);
                    comm.Parameters.AddWithValue($"@{nameof(u.Email)}",u.Email);
                    comm.Parameters.AddWithValue($"@{nameof(u.Clave)}",u.Clave);
                    comm.Parameters.AddWithValue($"@{nameof(u.Rol)}",u.Rol);
                    conn.Open();
                    res=Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    u.IdUsuario=res;
                }
            }
            return res;
        }

       public int Baja(int id)
        {
            var res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"DELETE FROM Usuario WHERE IdUsuario = @id";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@id",id);
                    conn.Open();
                    res = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }

        public int Editar(Usuario u)
        {
            int res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = $"Update Usuario Set Nombre=@nombre,Apellido=@apellido,Email=@Email,Rol=@Rol where IdUsuario = @id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue($"@{nameof(u.Nombre)}",u.Nombre);
                    comm.Parameters.AddWithValue($"@{nameof(u.Apellido)}",u.Apellido);
                    comm.Parameters.AddWithValue($"@{nameof(u.Email)}",u.Email);
                    comm.Parameters.AddWithValue($"@{nameof(u.Rol)}",u.Rol);
                    conn.Open();
                    res = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }
        public Usuario ObtenerPorId(int id)
        {
            Usuario res = null;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"Select IdUsuario,Nombre,Apellido,Email,Rol From Usuario where IdUsuario = @id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@id",id);
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    if(reader.Read())
                    {
                        res = new Usuario
                        {
                            IdUsuario = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Email = reader.GetString(3),
                            Rol = reader.GetInt32(4),
                        };
                    }
                    conn.Close();
                }
            }
            return res;
        }

        public Usuario ObtenerPorEmail(string email)
        {
            Usuario res = null;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = $"SELECT IdUsuario, Nombre, Apellido, Email, Clave, Rol FROM Usuario" +
                    $" WHERE Email=@email";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@email",email);
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    if(reader.Read())
                    {
                        res = new Usuario
                        {
                            IdUsuario = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Email = reader.GetString(3),
                            Clave = reader.GetString(4),
                            Rol = reader.GetInt32(5),
                        };
                    }
                    conn.Close();
                }
            }
            return res;
        }
    }

    
}
