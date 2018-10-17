using SGE.Entidades.Dispositivos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Usuarios
{
    [Table(name: "Usuario")]
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public DateTime FechaAlta { get; set; }
        //TODO: este  notMapped me hace ruido, verificar luego la consistencia de este atributo
        //[NotMapped]
        public virtual Direccion Direccion { get; set; } //one to one
        public string MensajeDeErrorLogueo { get; set; }

        public Usuario()
        {
            this.FechaAlta = DateTime.Now;
        }
    }
}
