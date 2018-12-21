using System;

namespace SGE.Entidades.Sensores.Sensores {
    public class VelocidadVentilador : SensorFisico {
        public override Medicion RealizarMedicion() {
            return new Medicion() {
                Unidad = UnidadEnum.OTROS,
                Valor = this.Dispositivo.VelocidadVentilador.ToString(),
                FechaRegistro = DateTime.Now
            };
        }
    }
}
