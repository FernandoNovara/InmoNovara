using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InmoNovara.Models
{
        public class RepositorioInquilino
    {
        String ConnectionString = "Server=localhost;User=root;Password=;Database=InmoNovara;SslMode=none";

        public RepositorioInquilino()
        {

        }

        public IList<Inquilino> ObtenerInquilino()
        {
            var res = new List<Inquilino>();
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"Select Id,Nombre,Dni,LugarTrabajo,Direccion,Correo,Telefono From Inquilino;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while(reader.Read())
                    {
                        var I = new Inquilino
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Dni = reader.GetString(2),
                            LugarTrabajo = reader.GetString(3),
                            Direccion = reader.GetString(4),
                            Correo = reader.GetString(5),
                            Telefono = reader.GetString(6)
                        };
                        res.Add(I);
                    }
                    conn.Close();
                }
            }
            return res;
        }

        public int Alta(Inquilino I)
        {
            var res = 0;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"insert into Inquilino (Nombre,Dni,LugarTrabajo,Direccion,Correo,Telefono) 
                            values (@{nameof(I.Nombre)},@{nameof(I.Dni)},@{nameof(I.LugarTrabajo)},@{nameof(I.Direccion)},@{nameof(I.Correo)},@{nameof(I.Telefono)});
                            Select last_Insert_Id();";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {

                    comm.Parameters.AddWithValue($"@{nameof(I.Nombre)}",I.Nombre);
                    comm.Parameters.AddWithValue($"@{nameof(I.Dni)}",I.Dni);
                    comm.Parameters.AddWithValue($"@{nameof(I.LugarTrabajo)}",I.LugarTrabajo);
                    comm.Parameters.AddWithValue($"@{nameof(I.Direccion)}",I.Direccion);
                    comm.Parameters.AddWithValue($"@{nameof(I.Correo)}",I.Correo);
                    comm.Parameters.AddWithValue($"@{nameof(I.Telefono)}",I.Telefono);   
                    conn.Open();
                    res=Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    I.Id=res;
                }
            }
            return res;
        }

       public int Baja(int id)
        {
            var res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"DELETE FROM Inquilino WHERE Id = @id";
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

        public int Editar(Inquilino I)
        {
            int res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = $"Update Inquilino Set Nombre=@nombre,Dni=@dni,LugarTrabajo=@LugarTrabajo,Direccion=@direccion,Correo=@correo,Telefono=@telefono where Id = @id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@nombre",I.Nombre);
                    comm.Parameters.AddWithValue("@dni",I.Dni);
                    comm.Parameters.AddWithValue("@LugarTrabajo",I.LugarTrabajo);
                    comm.Parameters.AddWithValue("@direccion",I.Direccion);
                    comm.Parameters.AddWithValue("@correo",I.Correo);
                    comm.Parameters.AddWithValue("@telefono",I.Telefono);   
                    comm.Parameters.AddWithValue("@id",I.Id);
                    conn.Open();
                    res = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }
        public Inquilino ObtenerPorId(int id)
        {
            Inquilino res = null;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @"Select Id,Nombre,Dni,LugarTrabajo,Direccion,Correo,Telefono From Inquilino where Id = @id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@id",id);
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    if(reader.Read())
                    {
                        res = new Inquilino
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Dni = reader.GetString(2),
                            LugarTrabajo = reader.GetString(3),
                            Direccion = reader.GetString(4),
                            Correo = reader.GetString(5),
                            Telefono = reader.GetString(6)
                        };
                    }
                    conn.Close();
                }
            }
            return res;
        }
    }

}