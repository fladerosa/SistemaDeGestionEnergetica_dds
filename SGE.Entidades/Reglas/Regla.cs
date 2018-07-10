using System;
using System.Collections.Generic;
using SGE.Entidades.Acciones;

namespace SGE.Entidades.Reglas
{
    public class Regla
    {
        #region Propiedades

        string nombre { get; set; }
        List<Condicion> condiciones { get; set; }
        List<IAccion> acciones { get; set; }

        #endregion Propiedades

        #region Constructores

        public Regla(string nombre, List<Condicion> condiciones, List<IAccion> acciones)
        {
            this.nombre = nombre;
            this.condiciones = condiciones;
            this.acciones = acciones;
        }

        #endregion

        #region Metodos

        public void Ejecutar()
        {
            bool seVerificanCondiciones = true;

            for (int i = 0; i < this.condiciones.Count; i++)
            {
                if (!this.condiciones[0].Evaluar())
                {
                    seVerificanCondiciones = false;
                    i = this.condiciones.Count;
                }
            }

            if (seVerificanCondiciones)
            {
                foreach (IAccion accion in this.acciones)
                    accion.Ejecutar();
            }
        }
       
        #endregion Metodos
    }
}
