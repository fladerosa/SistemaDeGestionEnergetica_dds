using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Actuadores
{
    public class AccionApagar : Actuador
    {
        public AccionApagar(string nombre, Inteligente dispositivo) : base(nombre, dispositivo)
        {
        }

        public override void Ejecutar()
        {
            this.Dispositivo.Apagar();
        }
    }
}
