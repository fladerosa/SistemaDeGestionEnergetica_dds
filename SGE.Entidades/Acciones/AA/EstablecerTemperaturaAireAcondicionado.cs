using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.WebconAutenticacion.Dispositivos;

namespace SGE.WebconAutenticacion.Acciones.AA
{
    public class EstablecerTemperaturaAireAcondicionado : Accion
    {
        #region Campos

        private AireAcondicionado dispositivo;
        private int temperatura;

        #endregion

        #region Constructores

        public EstablecerTemperaturaAireAcondicionado(AireAcondicionado dispositivo, int valorTemperatura)
        {
            this.dispositivo = dispositivo;
            this.temperatura = valorTemperatura;
        }
        public EstablecerTemperaturaAireAcondicionado()
        {
        }

        #endregion

        #region Ejecución

        public void Ejecutar()
        {
            this.dispositivo.EstablecerTemperatura(this.temperatura);
        }

        #endregion
    }
}
