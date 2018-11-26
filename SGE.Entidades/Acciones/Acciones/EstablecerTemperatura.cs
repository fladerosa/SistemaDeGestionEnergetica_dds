namespace SGE.Entidades.Acciones.Acciones {
    public class EstablecerTemperatura : Accion {
        public override void Ejecutar(string valor) {
            decimal resultado = 0;
            if (decimal.TryParse(valor, out resultado)) {
                this.Dispositivo.Temperatura = resultado;
            }
        }

    }
}
