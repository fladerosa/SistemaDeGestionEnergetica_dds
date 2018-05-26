using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Actuadores
{
    public class AccionSubirIntensidad : Actuador
    {
        public AccionSubirIntensidad(string nombre, Inteligente dispositivo) : base(nombre, dispositivo)
        {
        }

        public override void Ejecutar()
        {
            this.Dispositivo.SubirIntensidad();
        }
    }
}
