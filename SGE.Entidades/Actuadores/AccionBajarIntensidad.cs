using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Actuadores
{
    public class AccionBajarIntensidad : Actuador
    {
        public AccionBajarIntensidad(string nombre, Inteligente dispositivo) : base(nombre, dispositivo)
        {
        }

        public override void Ejecutar()
        {
            this.Dispositivo.BajarIntensidad();
        }
    }
}
