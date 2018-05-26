using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Actuadores
{
    public class AccionEncender : Actuador
    {
        public AccionEncender(string nombre, Inteligente dispositivo) : base(nombre, dispositivo)
        {
        }

        public override void Ejecutar()
        {
            this.Dispositivo.Encender();
        }
    }
}
