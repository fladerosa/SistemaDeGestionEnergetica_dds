using System;

namespace SGE.Entidades.Acciones.Acciones {
    public class DecrementarVelocidadVentilador : Accion {
        public override void Ejecutar(string valor) {
            int resultado = 0;
            if (Int32.TryParse(valor, out resultado)) {
                Dispositivo.VelocidadVentilador -= resultado;
            }
        }
    }
}
