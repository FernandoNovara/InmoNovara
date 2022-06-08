using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmoNovara.Models
{
    public class Pago
    {
        [Key]
        [Display(Name = "Codigo de pago")]
        public Int32 IdPago {get;set;}

        [Display(Name = "Numero de Contrato")]
        public Int32 IdContrato {get;set;}

        [Display(Name = "Fecha de Pago")]
        public DateTime FechaPago {get;set;}
        
        public double Importe {get;set;}
    }
}