using SGE.Entidades.Dispositivos;

namespace SGE.Entidades
{
    public abstract class Actuador
    {
        protected string Nombre { get; set; }
        protected Inteligente Dispositivo { get; set; }

        protected Actuador(string nombre, Inteligente dispositivo)
        {
            this.Nombre = nombre;
            this.Dispositivo = dispositivo;
        }

        public abstract void Ejecutar();
    }
}
