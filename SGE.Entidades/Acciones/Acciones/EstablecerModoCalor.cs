﻿using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Acciones.Acciones {
    public class EstablecerModoCalor : Accion {

        public override void Ejecutar() {
            this.Dispositivo.EstadoInterno = EstadosInternosDispositivo.modoCalor;
        }
    }
}
