namespace SGE.Entidades.Acciones.Acciones {

    public class Encender : Accion {
        public override void Ejecutar() {
            this.Dispositivo.Encender();
        }
    }
}
