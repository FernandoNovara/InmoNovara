using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InmoNovara.Models
{
    public class RepositorioContrato
    {
        String ConnectionString = "Server=localhost;User=root;Password=;Database=InmoNovara;SslMode=none";

        public RepositorioContrato()
        {

        }

        public IList<Contrato> ObtenerContrato()
        {
            var res = new List<Contrato>();
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @$"SELECT contrato.IdContrato,FechaInicio,FechaFinal,contrato.IdInmueble,inmueble.Tipo,contrato.IdInquilino,inquilino.Nombre,inquilino.Dni,contrato.IdGarante,garante.NombreGarante,garante.Dni
                                FROM contrato
                                JOIN inmueble On contrato.IdInmueble = inmueble.IdInmueble
                                JOIN inquilino On contrato.IdInquilino= inquilino.Id
                                JOIN garante on contrato.IdGarante = garante.IdGarante;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while(reader.Read())
                    {
                        var I = new Contrato
                        {
                            IdContrato = reader.GetInt32(0),
                            FechaInicio = reader.GetDateTime(1),
                            FechaFinal = reader.GetDateTime(2),
                            inmueble = new Inmueble
                            {
                                IdInmueble = reader.GetInt32(3),
                                Tipo = reader.GetString(4)
                            },
                            inquilino = new Inquilino
                            {
                                Id = reader.GetInt32(5),
                                Nombre = reader.GetString(6),
                                Dni = reader.GetString(7)
                            },
                            garante = new Garante
                            {
                                IdGarante = reader.GetInt32(8),
                                NombreGarante = reader.GetString(9),
                                Dni = reader.GetString(10)
                            }
                        };
                        res.Add(I);
                    }
                    conn.Close();
                }
            }
            return res;
        }

        public int Alta(Contrato I)
        {
            var res = 0;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"insert into Contrato (IdInmueble,IdInquilino,IdGarante,FechaInicio,FechaFinal) 
                            values (@{nameof(I.IdInmueble)},@{nameof(I.IdInquilino)},@{nameof(I.IdGarante)},@{nameof(I.FechaInicio)},@{nameof(I.FechaFinal)});
                            Select last_Insert_Id();";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {

                    comm.Parameters.AddWithValue($"@{nameof(I.IdInmueble)}",I.IdInmueble);
                    comm.Parameters.AddWithValue($"@{nameof(I.IdInquilino)}",I.IdInquilino);
                    comm.Parameters.AddWithValue($"@{nameof(I.IdGarante)}",I.IdGarante);
                    comm.Parameters.AddWithValue($"@{nameof(I.FechaInicio)}",I.FechaInicio);
                    comm.Parameters.AddWithValue($"@{nameof(I.FechaFinal)}",I.FechaFinal);
                    conn.Open();
                    res=Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    I.IdContrato=res;
                }
            }
            return res;
        }

       public int Baja(int id)
        {
            var res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"DELETE FROM Contrato WHERE IdContrato = @id";
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

        public int Editar(Contrato I)
        {
            int res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = $"Update Contrato Set IdInmueble=@IdInmueble,IdInquilino=@IdInquilino,IdGarante=@IdGarante,FechaInicio=@FechaInicio,FechaFinal=@FechaFinal where IdContrato = @Id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue($"@{nameof(I.IdInmueble)}",I.IdInmueble);
                    comm.Parameters.AddWithValue($"@{nameof(I.IdInquilino)}",I.IdInquilino);
                    comm.Parameters.AddWithValue($"@{nameof(I.IdGarante)}",I.IdGarante);   
                    comm.Parameters.AddWithValue($"@{nameof(I.FechaInicio)}",I.FechaInicio);
                    comm.Parameters.AddWithValue($"@{nameof(I.FechaFinal)}",I.FechaFinal);  
                    comm.Parameters.AddWithValue("@Id",I.IdContrato);
                    conn.Open();
                    res = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }
        public Contrato ObtenerPorId(int id)
        {
            Contrato res = null;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @$"SELECT contrato.IdContrato,FechaInicio,FechaFinal,contrato.IdInmueble,inmueble.Tipo,contrato.IdInquilino,inquilino.Nombre,inquilino.Dni,contrato.IdGarante,garante.Nombre,garante.Dni
                                FROM contrato
                                JOIN inmueble On contrato.IdInmueble = inmueble.IdInmueble
                                JOIN inquilino On contrato.IdInquilino= inquilino.Id
                                JOIN garante on contrato.IdGarante = garante.IdGarante where contrato.IdContrato = @id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@id",id);
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    if(reader.Read())
                    {
                        res = new Contrato
                        {
                            IdContrato = reader.GetInt32(0),
                            FechaInicio = reader.GetDateTime(1),
                            FechaFinal = reader.GetDateTime(2),
                            inmueble = new Inmueble
                            {
                                IdInmueble = reader.GetInt32(3),
                                Tipo = reader.GetString(4)
                            },
                            inquilino = new Inquilino
                            {
                                Id = reader.GetInt32(5),
                                Nombre = reader.GetString(6),
                                Dni = reader.GetString(7)
                            },
                            garante = new Garante
                            {
                                IdGarante = reader.GetInt32(8),
                                NombreGarante = reader.GetString(9),
                                Dni = reader.GetString(10)
                            }
                        };
                    }
                    conn.Close();
                }
            }
            return res;
        }

        public IList<Contrato> ObtenerPorIdInmueble(int id)
        {
            var res = new List<Contrato>();
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @$"SELECT contrato.IdContrato,FechaInicio,FechaFinal,contrato.IdInmueble,inmueble.Tipo,contrato.IdInquilino,inquilino.Nombre,inquilino.Dni,contrato.IdGarante,garante.Nombre,garante.Dni
                                FROM contrato
                                JOIN inmueble On contrato.IdInmueble = inmueble.IdInmueble
                                JOIN inquilino On contrato.IdInquilino= inquilino.Id
                                JOIN garante on contrato.IdGarante = garante.IdGarante where contrato.IdInmueble = @id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@id",id);
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    if(reader.Read())
                    {
                        var c = new Contrato
                        {
                            IdContrato = reader.GetInt32(0),
                            FechaInicio = reader.GetDateTime(1),
                            FechaFinal = reader.GetDateTime(2),
                            inmueble = new Inmueble
                            {
                                IdInmueble = reader.GetInt32(3),
                                Tipo = reader.GetString(4)
                            },
                            inquilino = new Inquilino
                            {
                                Id = reader.GetInt32(5),
                                Nombre = reader.GetString(6),
                                Dni = reader.GetString(7)
                            },
                            garante = new Garante
                            {
                                IdGarante = reader.GetInt32(8),
                                NombreGarante = reader.GetString(9),
                                Dni = reader.GetString(10)
                            }
                        };
                        res.Add(c);
                    }
                    conn.Close();
                }
            }
            return res;
        }

    }
}