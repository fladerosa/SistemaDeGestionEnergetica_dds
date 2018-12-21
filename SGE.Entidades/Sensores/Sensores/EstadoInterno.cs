using System;

namespace SGE.Entidades.Sensores.Sensores {
    public class EstadoInterno : SensorFisico {
        public override Medicion RealizarMedicion() {
            return new Medicion() {
                Unidad = UnidadEnum.OTROS,
                Valor = this.Dispositivo.EstadoInterno.ToString(),
                FechaRegistro = DateTime.Now
            };
        }
    }
}
