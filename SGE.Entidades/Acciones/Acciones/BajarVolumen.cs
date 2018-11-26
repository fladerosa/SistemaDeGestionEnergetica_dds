namespace SGE.Entidades.Acciones.Acciones {
    public class BajarVolumen : Accion {
        public override void Ejecutar(string valor) {
            int resultado = 0;
            if (int.TryParse(valor, out resultado)) {
                Dispositivo.Volumen -= resultado;
            }
            if (this.Dispositivo.Volumen < 0) {
                this.Dispositivo.Volumen = 0;
            }
        }
    }
}
