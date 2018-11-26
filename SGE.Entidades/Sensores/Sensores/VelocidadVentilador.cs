using System;

namespace SGE.Entidades.Sensores.Sensores {
    public class VelocidadVentilador : Sensor{
        public override Medicion RealizarMedicion() {
            return new Medicion() {
                Unidad = UnidadEnum.OTROS,
                Valor = this.Dispositivo.VelocidadVentilador.ToString(),
                FechaRegistro = DateTime.Now
            };
        }
    }
}
