using System;
using System.ComponentModel.DataAnnotations;

namespace InmoNovara.Models
{
    public class Pago
    {
        public Int32 IdPago {get;set;}
        public Int32 IdContrato {get;set;}
        public DateTime FechaPago {get;set;}
        public double Importe {get;set;}
    }
}