using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmoNovara.Models
{
    public class Contrato
    {
        [Key]
        [Display(Name = "Cod. Contrato")]
        public int IdContrato {get;set;}
        
        [Display(Name = "Cod. Inmueble")]
        public int IdInmueble {get;set;}
        
        [ForeignKey(nameof(IdInmueble))]
        public Inmueble inmueble {get;set;}
        
        [Display(Name = "Nom. Inquilino")]
        public int IdInquilino {get;set;}
        
        [ForeignKey(nameof(IdInquilino))]
        public Inquilino inquilino {get;set;}
        
        [Display(Name = "Nom. Garante")]
        public int IdGarante {get;set;}
        
        [ForeignKey(nameof(IdGarante))]
        public Garante garante {get;set;}
        
        [Display(Name = "Fecha Inicio")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio {get;set;}
        
        [Display(Name = "Fecha Final")]
        [DataType(DataType.Date)]
        public DateTime FechaFinal {get;set;}

    }
}