using SGE.Entidades.Categorias;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Simplex;
using SGE.Entidades.Transformadores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public virtual enum_TipoDocumento TipoDocumento { get; set; } 
        
        public virtual ICollection<Telefono> Telefonos { get; set; } // one to many con Cliente
        public int? CategoriaId { get; set; } //fk con tabla cliente
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; } // one to many con  Categoria
        public virtual ICollection<Inteligente> Inteligentes { get; set; } //many to many con Dispositivo
        public virtual ICollection<Estandar> Estandars { get; set; } //many to many con Dispositivo

        public virtual ICollection<Activacion> RegistroDeActivaciones { get; set; } //one to many con Activacion

        public Cliente()
        {
            this.Inteligentes = new HashSet<Inteligente>();
            //this.Inteligentes = new List<Inteligente>();
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

        public decimal HogarEficiente() {
            SimplexBuilder simplex = new SimplexBuilder();
            simplex.AgregarRestriccion(new KeyValuePair<string, string>("DDEAEA7C1ADE458991D496812D5D41FA", "elem_1"), 204);
            simplex.AgregarRestriccion(new KeyValuePair<string, string>("A0BA3245EAFC4EC994CC841698B835C0", "elem_2"), 40);
            simplex.Resolver();

            return (decimal)simplex.Resultado["ConsumoRestanteTotal"];
        }

        public enum enum_TipoDocumento {
            [Display(Name = "DNI")]
            DNI = 1,
            [Display(Name = "CUIL")]
            CUIL = 2,
            [Display(Name = "PASAPORTE")]
            PASAPORTE = 3
        }

        #endregion
    }
}
