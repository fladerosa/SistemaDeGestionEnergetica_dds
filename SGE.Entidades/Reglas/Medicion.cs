using System;

namespace SGE.Entidades.Reglas
{
    public class Medicion
    {
        #region Propiedades

        public decimal Valor { get; set; }
        public UnidadEnum Unidad { get; set; }
        public DateTime FechaRegistro { get; set; }

        #endregion

        #region Constructores

        public Medicion(decimal valor, UnidadEnum unidad)
        {
            this.Valor = valor;
            this.Unidad = unidad;
            this.FechaRegistro = DateTime.Now;
        }

        #endregion
    }
}
