using System;
using System.Collections.Generic;
using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Reglas.ValueProviders
{
    public class SensorTemperaturaAireAcondicionado: IValueProvider
    {
        #region Campos

        AireAcondicionado dispositivo;

        #endregion

        #region Constructores

        public SensorTemperaturaAireAcondicionado(AireAcondicionado dispositivo)
        {
            this.dispositivo = dispositivo;
        }

        #endregion

        #region Metodos

        public decimal ObtenerValor()
        {
            return this.dispositivo.ObtenerTemperaturaActual();
        }

        #endregion
    }
}
