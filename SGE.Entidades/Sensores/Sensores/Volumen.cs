using System;

namespace SGE.Entidades.Sensores.Sensores {
    public class Volumen : Sensor{
        public override Medicion RealizarMedicion() {
            return new Medicion() {
                Unidad = UnidadEnum.OTROS,
                Valor = this.Dispositivo.Volumen.ToString(),
                FechaRegistro = DateTime.Now
            };
        }
    }
}
