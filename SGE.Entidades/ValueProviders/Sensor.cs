using System;
using System.Collections.Generic;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Drivers.Interfaces;

namespace SGE.Entidades.ValueProviders
{
    public abstract class Sensor: IValueProvider
    {
        #region Campos

        decimal ultimaMedicion;
        List<Inteligente> dispositivos = new List<Inteligente>();

        #endregion

        #region Observer

        public void Agregar(Inteligente dispositivo)
        {
            this.dispositivos.Add(dispositivo);
        }

        public void Quitar(Inteligente dispositivo)
        {
            this.dispositivos.Remove(dispositivo);
        }

        public void NotificarCambio()
        {
            foreach(Inteligente dispositivo in this.dispositivos)
            {
                dispositivo.NotificarNuevaMedicion(this.ultimaMedicion);
            }
        }

        #endregion

        #region Medición

        public decimal Medir()
        {
            this.ultimaMedicion = this.RealizarMedicion();
            this.NotificarCambio();
            return this.ultimaMedicion;
        }

        public abstract decimal RealizarMedicion();

        #endregion

        #region Implementación de métodos IValueProvider

        public decimal ObtenerValor()
        {
            return this.ultimaMedicion;
        }

        #endregion
    }
}
