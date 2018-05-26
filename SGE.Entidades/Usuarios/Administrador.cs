using System;

namespace SGE.Entidades.Usuarios
{
    public class Administrador: Usuario
    {
        public int Antiguedad()
        {
            return (int)Math.Truncate((DateTime.Now - this.FechaAlta).TotalDays);
        }
    }
}
