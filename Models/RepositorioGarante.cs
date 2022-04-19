using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InmoNovara.Models
{
        public class RepositorioGarante
    {
        String ConnectionString = "Server=localhost;User=root;Password=;Database=InmoNovara;SslMode=none";

        public RepositorioGarante()
        {

        }

        public IList<Garante> ObtenerGarante()
        {
            var res = new List<Garante>();
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"Select IdGarante,Nombre,Dni,Direccion,Correo,Telefono From Garante;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while(reader.Read())
                    {
                        var I = new Garante
                        {
                            IdGarante = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Dni = reader.GetString(2),
                            Direccion = reader.GetString(3),
                            Correo = reader.GetString(4),
                            Telefono = reader.GetString(5)
                        };
                        res.Add(I);
                    }
                    conn.Close();
                }
            }
            return res;
        }

        public int Alta(Garante I)
        {
            var res = 0;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"insert into Garante (Nombre,Dni,Direccion,Correo,Telefono) 
                            values (@{nameof(I.Nombre)},@{nameof(I.Dni)},@{nameof(I.Direccion)},@{nameof(I.Correo)},@{nameof(I.Telefono)});
                            Select last_Insert_Id();";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {

                    comm.Parameters.AddWithValue($"@{nameof(I.Nombre)}",I.Nombre);
                    comm.Parameters.AddWithValue($"@{nameof(I.Dni)}",I.Dni);
                    comm.Parameters.AddWithValue($"@{nameof(I.Direccion)}",I.Direccion);
                    comm.Parameters.AddWithValue($"@{nameof(I.Correo)}",I.Correo);
                    comm.Parameters.AddWithValue($"@{nameof(I.Telefono)}",I.Telefono);   
                    conn.Open();
                    res=Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    I.IdGarante=res;
                }
            }
            return res;
        }

       public int Baja(int id)
        {
            var res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"DELETE FROM Garante WHERE IdGarante = @idGarante";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@idGarante",id);
                    conn.Open();
                    res = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }

        public int Editar(Garante I)
        {
            int res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = $"Update Garante Set Nombre=@nombre,Dni=@dni,Direccion=@direccion,Correo=@correo,Telefono=@telefono where IdGarante = @idGarante;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@nombre",I.Nombre);
                    comm.Parameters.AddWithValue("@dni",I.Dni);
                    comm.Parameters.AddWithValue("@direccion",I.Direccion);
                    comm.Parameters.AddWithValue("@correo",I.Correo);
                    comm.Parameters.AddWithValue("@telefono",I.Telefono);   
                    comm.Parameters.AddWithValue("@idGarante",I.IdGarante);
                    conn.Open();
                    res = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }
        public Garante ObtenerPorId(int id)
        {
            Garante res = null;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"Select IdGarante,Nombre,Dni,Direccion,Correo,Telefono From Garante where IdGarante = @id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@id",id);
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    if(reader.Read())
                    {
                        res = new Garante
                        {
                            IdGarante = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Dni = reader.GetString(2),
                            Direccion = reader.GetString(3),
                            Correo = reader.GetString(4),
                            Telefono = reader.GetString(5)
                        };
                    }
                    conn.Close();
                }
            }
            return res;
        }
    }

}