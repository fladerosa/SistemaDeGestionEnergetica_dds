using System;

namespace SGE.Entidades.Dispositivos
{
    public class Activacion
    {
        public EstadoDispositivo Estado { get; private set; }
        public DateTime FechaDeRegistro { get; set; }

        public Activacion(EstadoDispositivo estado)
        {
            this.FechaDeRegistro = DateTime.Now;
            this.Estado = estado;
        }
    }
}
