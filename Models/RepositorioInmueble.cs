using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InmoNovara.Models
{
        public class RepositorioInmueble
    {
        String ConnectionString = "Server=localhost;User=root;Password=;Database=InmoNovara;SslMode=none";

        public RepositorioInmueble()
        {

        }

        public IList<Inmueble> ObtenerInmueble()
        {
            var res = new List<Inmueble>();
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @$"SELECT IdInmueble,Tipo,Ambiente,Tamaño,Id,Nombre,Apellido FROM `inmueble` JOIN propietarios on inmueble.IdPropietario = propietarios.Id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while(reader.Read())
                    {
                        var I = new Inmueble
                        {
                            IdInmueble = reader.GetInt32(0),
                            Tipo = reader.GetString(1),
                            Tamaño = reader.GetString(2),
                            Ambiente = reader.GetString(3),
                            Propietario = new Propietario
                            {
                                Id = reader.GetInt32(4),
                                Nombre = reader.GetString(5),
                                Apellido = reader.GetString(6)
                            }
                        };
                        res.Add(I);
                    }
                    conn.Close();
                }
            }
            return res;
        }

        public int Alta(Inmueble I)
        {
            var res = 0;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"insert into Inmueble (Tipo,Tamaño,Ambiente,IdPropietario) 
                            values (@{nameof(I.Tipo)},@{nameof(I.Tamaño)},@{nameof(I.Ambiente)},@{nameof(I.IdPropietario)});
                            Select last_Insert_Id();";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {

                    comm.Parameters.AddWithValue($"@{nameof(I.Tipo)}",I.Tipo);
                    comm.Parameters.AddWithValue($"@{nameof(I.Tamaño)}",I.Tamaño);
                    comm.Parameters.AddWithValue($"@{nameof(I.Ambiente)}",I.Ambiente);
                    comm.Parameters.AddWithValue($"@{nameof(I.IdPropietario)}",I.IdPropietario);
                    conn.Open();
                    res=Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    I.IdInmueble=res;
                }
            }
            return res;
        }

       public int Baja(int id)
        {
            var res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"DELETE FROM Inmueble WHERE Id = @id";
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

        public int Editar(Inmueble I)
        {
            int res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = $"Update Inmueble Set Tipo=@Tipo,Tamaño=@Tamaño,Ambiente=@Ambiente where IdInmueble = @IdInmueble;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue($"@{nameof(I.Tipo)}",I.Tipo);
                    comm.Parameters.AddWithValue($"@{nameof(I.Tamaño)}",I.Tamaño);
                    comm.Parameters.AddWithValue($"@{nameof(I.Ambiente)}",I.Ambiente);   
                    comm.Parameters.AddWithValue("@IdInmueble",I.IdInmueble);
                    conn.Open();
                    res = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }
        public Inmueble ObtenerPorId(int id)
        {
            Inmueble res = null;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @$"SELECT IdInmueble,Tipo,Ambiente,Tamaño,Id,Nombre,Apellido FROM `inmueble` JOIN propietarios on inmueble.IdPropietario = propietarios.Id && inmueble.IdInmueble = @id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@id",id);
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    if(reader.Read())
                    {
                        res = new Inmueble
                        {
                            IdInmueble = reader.GetInt32(0),
                            Tipo = reader.GetString(1),
                            Ambiente = reader.GetString(2),
                            Tamaño = reader.GetString(3),
                            Propietario = new Propietario
                            {
                                Id = reader.GetInt32(4),
                                Nombre = reader.GetString(5),
                                Apellido = reader.GetString(6)
                            }
                        };
                    }
                    conn.Close();
                }
            }
            return res;
        }
    }

}