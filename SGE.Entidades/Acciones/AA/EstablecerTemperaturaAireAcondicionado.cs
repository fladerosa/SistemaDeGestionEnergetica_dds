using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Acciones.AA
{
    public class EstablecerTemperaturaAireAcondicionado : IAccion
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

        #endregion

        #region Ejecución

        public void Ejecutar()
        {
            this.dispositivo.EstablecerTemperatura(this.temperatura);
        }

        #endregion
    }
}
