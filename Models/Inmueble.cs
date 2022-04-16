using System;
using System.ComponentModel.DataAnnotations;

namespace InmoNovara.Models
{
    public class Inmueble
    {
        [Display(Name ="Codigo")]
        public Int32 IdInmueble {get;set;}
        public string Tipo {get;set;}
        public string Ambiente {get;set;}
        public string Tamaño {get;set;}
        [Display(Name ="Propietario")]
        public int IdPropietario{get;set;}
        public Propietario Propietario {get;set;}
    }
}