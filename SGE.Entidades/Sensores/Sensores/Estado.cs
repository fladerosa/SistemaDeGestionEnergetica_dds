using System;

namespace SGE.Entidades.Sensores.Sensores {
    public class Estado : Sensor {
        public override Medicion RealizarMedicion() {
            return new Medicion() {
                Unidad = UnidadEnum.OTROS,
                Valor = this.Dispositivo.Estado.ToString(),
                FechaRegistro = DateTime.Now
            };
        }
    }
}
