using SGE.Entidades.Categorias;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Transformadores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SGE.Entidades.Usuarios
{
    [Table(name: "Cliente")]
    public class Cliente : Usuario
    {
        #region Propiedades
        public string NumeroDocumento { get; set; }
        public double Latitud { get; set; }  
        public double Longitud { get; set; }

        public int TransformadorId { get; set; } // fk con tabla transformador
        [ForeignKey("TransformadorId")]
        public virtual Transformador Transformador { get; set; } // one to many con  Transformador
        public virtual int  TipoDocumentoId { get; set; } //one to many
        //public virtual TipoDocumento TipoDocumento { get; set; } //one to many
        public virtual enum_TipoDocumento TipoDocumento { get; set; } 
        
        public virtual ICollection<Telefono> Telefonos { get; set; } // one to many con Cliente
        public int CategoriaId { get; set; } //fk con tabla cliente
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; } // one to many con  Categoria

        public Cliente()
        {
            this.Inteligentes = new List<Inteligente>();
            this.Estandars = new List<Estandar>();
            this.Telefonos = new List<Telefono>();
        }

        public bool TieneDispositivosEncendidos()
        {
            return this.Inteligentes.Any(d => d.EstaPrendido);
        }

        public int CantidadDispositivosEncendidos()
        {
            return this.Inteligentes.Count(d => d.EstaPrendido);
        }

        public int CantidadDispositivosApagados()
        {
            return this.Inteligentes.Count(d => !d.EstaPrendido);
        }

        public int CantidadTotalDispositivos()
        {
            return this.Estandars.Count;
        }

        public enum enum_TipoDocumento {
            DNI,
            CUIL,
            PASAPORTE
        }

        #endregion
    }
}
