using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Entidades.Dispositivos;

namespace SGE.Entidades.Reglas
{
    public class Sensor
    {

        #region Propiedades
        //public string Nombre { get; set; }
        //public string TipoMagnitud { get; set; }
        //public Dispositivo Dispositivo { get; set; }
        //public Medicion Medicion { get; set; }

        public decimal Valor { get; set; }
        public string Unidad { get; set; }

        #endregion Propiedades


        #region Metodos
        //public decimal ObtenerMedicion()
        //{
        //    return this.Medicion.Valor;
        //}

        //public void ComunicarMedicion()
        //{
        //    Medicion medicionAcutual = new Medicion(this);
        //    this.Medicion = medicionAcutual;
        //}

        public decimal Medir()
        {
            return decimal.MaxValue;
        }

        #endregion Metodos
    }
}
