using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.WebconAutenticacion.Dispositivos;
using SGE.WebconAutenticacion.Managers;
using SGE.WebconAutenticacion.Reglas;

namespace SGE.WebconAutenticacion.Acciones
{
    public class ColocarEnAhorroEnergia: Accion
    {
        #region Campos
            
        private Inteligente dispositivo;
       
        #endregion

        #region Constructores

        public ColocarEnAhorroEnergia(Inteligente dispositivo)
        {
            this.dispositivo = dispositivo;
        }

        #endregion

        #region Ejecución

        public void Ejecutar()
        {
            this.dispositivo.ColocarEnAhorroEnergia();
        }

        #endregion
    }
}
