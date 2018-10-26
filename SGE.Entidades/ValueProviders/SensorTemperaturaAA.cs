using SGE.Entidades.Drivers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.ValueProviders
{
    public class SensorTemperaturaAA : Sensor
    {
        #region Campos

        IAireAcondicionadoDriver driver;

        #endregion

        #region Constructores

        public SensorTemperaturaAA(IAireAcondicionadoDriver driver)
        {
            this.driver = driver;
        }

        public SensorTemperaturaAA(decimal medicion, IAireAcondicionadoDriver driver)
        {
            this.ultimaMedicion = medicion;
            this.driver = driver;
        }
        #endregion

        #region Reimplementación de los métodos de Sensor

        public override decimal RealizarMedicion()
        {
            return this.driver.ObtenerTemperaturaActual();   
        }

        #endregion
    }
}
