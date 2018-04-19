using System;

namespace SGE.Entidades
{
    public abstract class Usuario
    {
        public Usuario()
        {
            this.FechaAlta = DateTime.Now;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Direccion { get; set; }
    }
}
