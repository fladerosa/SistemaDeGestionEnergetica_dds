//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SGE.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [DisplayName("Nombre de Usuario")]
        [Required(ErrorMessage ="Campo Obligatorio")]
        public string NombreUsuario { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Password { get; set; }
        public System.DateTime FechaAlta { get; set; }

        public string MensajeDeErrorLogueo { get; set; }
    }
}