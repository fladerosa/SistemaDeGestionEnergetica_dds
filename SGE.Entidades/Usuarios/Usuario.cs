using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Usuarios
{
    [Table(name: "Usuario")]
    public abstract class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public DateTime FechaAlta { get; set; }
        //   public string Direccion { get; set; }
        public virtual Direccion Direccion { get; set; } //one to one

        public Usuario()
        {
            this.FechaAlta = DateTime.Now;
        }
    }
}
