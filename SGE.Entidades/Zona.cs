using System;
using SGE.Entidades.Categorias;
using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ComponentModel.DataAnnotations.Schema;
using SGE.Entidades.Usuarios;

namespace SGE.Entidades
{
    [Table("Zona")]
    public class Zona
    {
        #region Propiedades

        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public decimal Radio { get; set; }
        public List<Transformador> Transformadores { get; set; } //one to many con Transformador
        public virtual Direccion Direccion { get; set; } // one to many con  Direccion

        public Zona()
        {
            this.Transformadores = new List<Transformador>();
        }

        public decimal ObtenerConsumo()
        {
            decimal consumo = 0;
            foreach (Transformador transformador in Transformadores)
            {
                consumo += transformador.ObtenerConsumo();
            }
            return consumo;
        }

        #endregion
    }
}
