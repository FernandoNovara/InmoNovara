using System;
using System.ComponentModel.DataAnnotations;

namespace InmoNovara.Models
{
    public class Propietario
    {
        [Display(Name = "Codigo")]
        public int Id {get;set;}
        public string Nombre {get;set;}
        public string Apellido {get;set;}
        [Display(Name = "Documento")]
        public string Dni {get;set;}
        public string Direccion {get;set;}
        public string Telefono {get;set;}
    }
}
