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

        public Dictionary<string, double> HogarEficiente() {
            if(Inteligentes.Count > 0) {
                SimplexNormal simplex = new SimplexNormal();

                foreach (Inteligente inteligente in Inteligentes) {
                    simplex.AgregarRestriccion(inteligente);
                }
                
                simplex.Resolver();

                return simplex.Resultado;
            }
            return null;
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
