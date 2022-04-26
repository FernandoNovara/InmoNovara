using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace InmoNovara.Models
{
        public class RepositorioPago
    {
        String ConnectionString = "Server=localhost;User=root;Password=;Database=InmoNovara;SslMode=none";

        public RepositorioPago()
        {

        }

        public IList<Pago> ObtenerPago()
        {
            var res = new List<Pago>();
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @$"SELECT IdPago,Pago.IdContrato,FechaPago,Importe FROM Pago JOIN Contrato on Pago.IdContrato = Contrato.IdContrato;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while(reader.Read())
                    {
                        var p = new Pago
                        {
                            IdPago = reader.GetInt32(0),
                            IdContrato = reader.GetInt32(1),
                            FechaPago = reader.GetDateTime(2),
                            Importe = reader.GetDouble(3),
                        };
                        res.Add(p);
                    }
                    conn.Close();
                }
            }
            return res;
        }

        public int Alta(Pago p)
        {
            var res = 0;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"insert into Pago (IdContrato,FechaPago,Importe) 
                            values (@{nameof(p.IdContrato)},@{nameof(p.FechaPago)},@{nameof(p.Importe)});
                            Select last_Insert_Id();";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue($"@{nameof(p.IdContrato)}",p.IdContrato);
                    comm.Parameters.AddWithValue($"@{nameof(p.FechaPago)}",p.FechaPago);
                    comm.Parameters.AddWithValue($"@{nameof(p.Importe)}",p.Importe);
                    conn.Open();
                    res=Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    p.IdPago=res;
                }
            }
            return res;
        }

       public int Baja(int id)
        {
            var res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = @$"DELETE FROM Pago WHERE IdPago = @id";
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

        public int Editar(Pago p)
        {
            int res = -1;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                String sql = $"Update Pago Set IdContrato=@IdContrato,FechaPago=@FechaPago,Importe=@Importe where IdPago = @IdPago;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue($"@{nameof(p.IdContrato)}",p.IdContrato);
                    comm.Parameters.AddWithValue($"@{nameof(p.FechaPago)}",p.FechaPago);
                    comm.Parameters.AddWithValue($"@{nameof(p.Importe)}",p.Importe);  
                    comm.Parameters.AddWithValue("@IdPago",p.IdPago);
                    conn.Open();
                    res = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return res;
        }
        public Pago ObtenerPorId(int id)
        {
            Pago res = null;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                string sql = @$"SELECT IdPago,Pago.IdContrato,FechaPago,Importe FROM Pago JOIN Contrato on Pago.IdContrato = Contrato.IdContrato && Pago.IdPago = @id;";
                using(MySqlCommand comm = new MySqlCommand(sql,conn))
                {
                    comm.Parameters.AddWithValue("@id",id);
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    if(reader.Read())
                    {
                        res = new Pago
                        {
                            IdPago = reader.GetInt32(0),
                            IdContrato = reader.GetInt32(1),
                            FechaPago = reader.GetDateTime(2),
                            Importe = reader.GetDouble(3),
                        };
                    }
                    conn.Close();
                }
            }
            return res;
        }
    }

}