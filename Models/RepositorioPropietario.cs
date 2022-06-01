using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InmoNovara.Models
{
    public class RepositorioPropietario
    {
        String ConnectionString = "Server=localhost;User=root;Password=;Database=InmoNovara;SslMode=none";

        public RepositorioPropietario()
        {

        }

        public IList<Propietario> ObtenerPropietario()
        {
            var res = new List<Propietario>();
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"Select Id,Nombre,Apellido,Dni,Email,Telefono,Clave From Propietarios;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while(reader.Read())
                    {
                        var p = new Propietario
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Email = reader.GetString(4),
                            Telefono = reader.GetString(5),
                            Clave = reader.GetString(6)
                        };
                        res.Add(p);
                    }
                    conn.Close();
                }
            }
            return res;
        }

        public int Alta(Propietario p)
        {
            var res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"insert into propietarios (Nombre,Apellido,Dni,Email,Telefono,Clave) 
                            Values (@{nameof(p.Nombre)},@{nameof(p.Apellido)},@{nameof(p.Dni)},@{nameof(p.Email)},@{nameof(p.Telefono)},@{nameof(p.Clave)});
                            Select last_Insert_Id();";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue($"@{nameof(p.Nombre)}",p.Nombre);
                    comm.Parameters.AddWithValue($"@{nameof(p.Apellido)}",p.Apellido);
                    comm.Parameters.AddWithValue($"@{nameof(p.Dni)}",p.Dni);
                    comm.Parameters.AddWithValue($"@{nameof(p.Email)}",p.Email);
                    comm.Parameters.AddWithValue($"@{nameof(p.Telefono)}",p.Telefono);
                    comm.Parameters.AddWithValue($"@{nameof(p.Clave)}",p.Clave);   
                    conn.Open();
                    res=Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    p.Id=res;
                }
            }
            return res;
        }

       public int Baja(int id)
        {
            var res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"DELETE FROM propietarios WHERE Id = @id";
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

        public int Editar(Propietario p)
        {
            int res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = $"Update propietarios Set Nombre=@nombre,Apellido=@apellido,Dni=@dni,Email=@email,Telefono=@telefono,Clave=@Clave where Id = @id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@nombre",p.Nombre);
                    comm.Parameters.AddWithValue("@apellido",p.Apellido);
                    comm.Parameters.AddWithValue("@dni",p.Dni);
                    comm.Parameters.AddWithValue("@email",p.Email);
                    comm.Parameters.AddWithValue("@telefono",p.Telefono); 
                    comm.Parameters.AddWithValue("@Clave",p.Clave);  
                    comm.Parameters.AddWithValue("@id",p.Id);
                    conn.Open();
                    res = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }
        public Propietario ObtenerPorId(int id)
        {
            Propietario res = null;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"Select Id,Nombre,Apellido,Dni,Email,Telefono,Clave From Propietarios where Id = @id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@id",id);
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    if(reader.Read())
                    {
                        res = new Propietario
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Email = reader.GetString(4),
                            Telefono = reader.GetString(5),
                            Clave = reader.GetString(6)
                        };
                    }
                    conn.Close();
                }
            }
            return res;
        }
    }

    
}
