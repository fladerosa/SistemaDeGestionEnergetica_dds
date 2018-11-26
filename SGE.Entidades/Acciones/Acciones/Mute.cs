namespace SGE.Entidades.Acciones.Acciones {
    public class Mute : Accion {

        public override void Ejecutar() {
            this.Dispositivo.Volumen = 0;
        }
    }
}
