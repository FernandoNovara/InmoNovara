using System;
using System.ComponentModel.DataAnnotations;

namespace InmoNovara.Models
{
    public class Contrato
    {
        [Display(Name = "Cod. Contrato")]
        public int IdContrato {get;set;}
        [Display(Name = "Cod. Inmueble")]
        public int IdInmueble {get;set;}
        public Inmueble inmueble {get;set;}
        [Display(Name = "Nom. Inquilino")]
        public int IdInquilino {get;set;}
        public Inquilino inquilino {get;set;}
        [Display(Name = "Nom. Garante")]
        public int IdGarante {get;set;}
        public Garante garante {get;set;}
        [Display(Name = "Fecha Inicio")]
        public DateTime FechaInicio {get;set;}
        [Display(Name = "Fecha Final")]
        public DateTime FechaFinal {get;set;}

    }
}