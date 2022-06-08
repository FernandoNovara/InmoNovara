using System;
using System.ComponentModel.DataAnnotations;

namespace InmoNovara.Models
{
    public class Garante
    {
        [Display(Name = "Codigo")]
        [Key]
        public Int32 IdGarante {get;set; }
        [Display(Name = "Nombre Completo")]
        public string NombreGarante {get;set;}
        [Display(Name = "Documento")]
        public string Dni {get;set;}
        public string Direccion {get;set;}
        public string Correo {get;set;}  
        public string TelefonoGarante {get;set;}
    }
}
