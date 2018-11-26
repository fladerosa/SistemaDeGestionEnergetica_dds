namespace SGE.Entidades.Acciones.Acciones {

    public class Apagar : Accion {

        public override void Ejecutar() {
            this.Dispositivo.Apagar();
        }
    }
}
