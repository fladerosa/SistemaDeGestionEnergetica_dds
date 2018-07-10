using System;
using SGE.Entidades.Categorias;
using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.Linq;

namespace SGE.Entidades.Usuarios
{
    public class Cliente: Usuario
    {
        #region Propiedades

        public TipoDocumento TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Telefono { get; set; }
        public Categoria Categoria { get; set; }
        public List<Inteligente> Inteligentes { get; set; }
        public List<Estandar> Estandars { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int TransformadorId { get; set; }


        public Cliente()
        {
            this.Inteligentes = new List<Inteligente>();
            this.Estandars = new List<Estandar>();
        }

        public bool TieneDispositivosEncendidos()
        {
            return this.Inteligentes.Any(d => d.EstaPrendido);
        }

        public int CantidadDispositivosEncendidos()
        {
            return this.Inteligentes.FindAll(d => d.EstaPrendido).Count;
        }

        public int CantidadDispositivosApagados()
        {
            return this.Inteligentes.FindAll(d => !d.EstaPrendido).Count;
        }

        public int CantidadTotalDispositivos()
        {
            return this.Inteligentes.Count;
        }

        #endregion
    }
}
