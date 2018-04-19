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

        public bool TieneDispositivosEncendidos
        {
            get
            {
                return this.Dispositivos.Any(d => d.EstaEncendido);
            }
        }

        public int CantidadDispositivosEncendidos
        {
            get
            {
                return this.Dispositivos.FindAll(d => d.EstaEncendido).Count;
            }
        }

        public int CantidadDispositivosApagados
        {
            get
            {
                return this.Dispositivos.FindAll(d => !d.EstaEncendido).Count;
            }
        }

        public int CantidadTotalDispositivos
        {
            get
            {
                return this.Dispositivos.Count;
            }
        }

        #endregion
    }
}
