namespace SGE.Entidades.Acciones.Acciones {
    public class SubirVolumen : Accion {
        public override void Ejecutar(string valor) {
            int resultado = 0;
            if (int.TryParse(valor, out resultado)) {
                Dispositivo.Volumen += resultado;
            }
            if (this.Dispositivo.Volumen > 100) {
                this.Dispositivo.Volumen = 100;
            }
        }
    }
}
