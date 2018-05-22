using SGE.Entidades.Dispositivos;
using System.Collections.Generic;
using System.Linq;

namespace SGE.Entidades
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
        public List<EstandarAdaptado>EstandarAdaptados { get; set; }


        public Cliente()
        {
            this.Inteligentes = new List<Inteligente>();
            this.Estandars = new List<Estandar>();
            this.EstandarAdaptados = new List<EstandarAdaptado>();
        }

        public bool TieneDispositivosEncendidos()
        {
            return this.Inteligentes.Any(d => d.EstaEncendido) || this.EstandarAdaptados.Any(d => d.EstaEncendido);
        }

        public int CantidadDispositivosEncendidos()
        {
            return this.Inteligentes.FindAll(d => d.EstaEncendido).Count + this.EstandarAdaptados.FindAll(d => d.EstaEncendido).Count;
        }

        public int CantidadDispositivosApagados()
        {
            return this.Inteligentes.FindAll(d => !d.EstaEncendido).Count + this.EstandarAdaptados.FindAll(d => !d.EstaEncendido).Count;
        }

        public int CantidadTotalDispositivos()
        {
            return this.Inteligentes.Count + this.EstandarAdaptados.Count;
        }

        #endregion
    }
}
