namespace SGE.Entidades.Acciones.Acciones {
    public class ColocarEnAhorroEnergia : Accion {

        public override void Ejecutar() {
            this.Dispositivo.ColocarEnAhorroEnergia();
        }
    }
}
