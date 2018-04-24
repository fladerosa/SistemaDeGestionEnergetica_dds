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
        public List<Dispositivo> Dispositivos { get; set; }


        public Cliente()
        {
            this.Dispositivos = new List<Dispositivo>();
        }

        public bool TieneDispositivosEncendidos()
        {
            return this.Dispositivos.Any(d => d.EstaEncendido);
        }

        public int CantidadDispositivosEncendidos()
        {
            return this.Dispositivos.FindAll(d => d.EstaEncendido).Count;
        }

        public int CantidadDispositivosApagados()
        {
            return this.Dispositivos.FindAll(d => !d.EstaEncendido).Count;
        }

        public int CantidadTotalDispositivos()
        {
            return this.Dispositivos.Count;
        }

        #endregion

        public Categoria Categoria1
        {
            get => default(Categoria);
            set
            {
            }
        }
    }
}
