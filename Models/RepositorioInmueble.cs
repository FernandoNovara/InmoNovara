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
                string sql = @$"SELECT IdInmueble,Uso,Tipo,Ambiente,Tamaño,Precio,Id,Nombre,Apellido FROM `inmueble` JOIN propietarios on inmueble.IdPropietario = propietarios.Id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while(reader.Read())
                    {
                        var I = new Inmueble
                        {
                            IdInmueble = reader.GetInt32(0),
                            Uso = reader.GetString(1),
                            Tipo = reader.GetString(2),
                            Ambiente = reader.GetString(3),
                            Tamaño = reader.GetString(4),
                            Precio = reader.GetDouble(5),
                            Propietario = new Propietario
                            {
                                Id = reader.GetInt32(6),
                                Nombre = reader.GetString(7),
                                Apellido = reader.GetString(8)
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
                String sql = @$"insert into Inmueble (Uso,Tipo,Tamaño,Ambiente,Precio,IdPropietario) 
                            values (@{nameof(I.Uso)},@{nameof(I.Tipo)},@{nameof(I.Tamaño)},@{nameof(I.Ambiente)},@{nameof(I.Precio)},@{nameof(I.IdPropietario)});
                            Select last_Insert_Id();";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue($"@{nameof(I.Uso)}",I.Uso);
                    comm.Parameters.AddWithValue($"@{nameof(I.Tipo)}",I.Tipo);
                    comm.Parameters.AddWithValue($"@{nameof(I.Tamaño)}",I.Tamaño);
                    comm.Parameters.AddWithValue($"@{nameof(I.Ambiente)}",I.Ambiente);
                    comm.Parameters.AddWithValue($"@{nameof(I.Precio)}",I.Precio);
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
                String sql = $"Update Inmueble Set Uso=@Uso,Tipo=@Tipo,Tamaño=@Tamaño,Ambiente=@Ambiente,Precio=@Precio where IdInmueble = @IdInmueble;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue($"@{nameof(I.Uso)}",I.Uso);
                    comm.Parameters.AddWithValue($"@{nameof(I.Tipo)}",I.Tipo);
                    comm.Parameters.AddWithValue($"@{nameof(I.Tamaño)}",I.Tamaño);
                    comm.Parameters.AddWithValue($"@{nameof(I.Ambiente)}",I.Ambiente);
                    comm.Parameters.AddWithValue($"@{nameof(I.Precio)}",I.Precio);   
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
                string sql = @$"Select IdInmueble,Uso,Tipo,Ambiente,Tamaño,Precio,Id,Nombre,Apellido FROM `inmueble` JOIN propietarios on inmueble.IdPropietario = propietarios.Id && inmueble.IdInmueble = @id;";
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
                            Uso = reader.GetString(1),
                            Tipo = reader.GetString(2),
                            Ambiente = reader.GetString(3),
                            Tamaño = reader.GetString(4),
                            Precio = reader.GetDouble(5),
                            Propietario = new Propietario
                            {
                                Id = reader.GetInt32(6),
                                Nombre = reader.GetString(7),
                                Apellido = reader.GetString(8)
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