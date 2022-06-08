using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmoNovara.Models
{
    public class Inmueble
    {
        [Display(Name ="Codigo")]
        [Key]
        public Int32 IdInmueble {get;set;}
        
        public string Direccion {get;set;}
        
        public string Uso {get;set;}
        
        public string Tipo {get;set;}
        
        public string Ambiente {get;set;}
        
        public string Tamaño {get;set;}
        
        public double Precio {get;set;}
        
        public string Estado {get;set;}
        
        [Display(Name ="Propietario")]
        public int IdPropietario{get;set;}
        
        [ForeignKey(nameof(IdPropietario))]
        public Propietario Propietario {get;set;}
    }
}