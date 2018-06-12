using System;
using System.Collections.Generic;

namespace SGE.Entidades.Reglas
{
    public class Sensor
    {
        #region Propiedades
        public UnidadEnum Unidad { get; set; }
        public List<Medicion> HistoricoMediciones { get; set; }

        public TipoUnidadEnum TipoUnidad { get; set; }


        #endregion Propiedades

        public Sensor(TipoUnidadEnum tipoUnidad,UnidadEnum unidad)
        {
            this.TipoUnidad = tipoUnidad;
            this.Unidad = unidad;
        }

        public Sensor(UnidadEnum unidad)
        {
            this.Unidad = unidad;
        }

        #region Metodos
        public decimal Medir()
        {
            decimal valor = (new Random()).Next(1, 20);
            this.HistoricoMediciones.Add(new Medicion(valor, this.Unidad));
            return valor;
        }
        #endregion Metodos
    }
}
