using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Acciones.Acciones {
    public class EstablecerModoFrio : Accion {

        public override void Ejecutar() {
            this.Dispositivo.EstadoInterno = EstadosInternosDispositivo.modoFrio;
        }

    }
}
