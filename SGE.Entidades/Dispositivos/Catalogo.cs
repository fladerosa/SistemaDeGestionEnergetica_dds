using SGE.Entidades.Contexto;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Dispositivos
{
    [Table(name: "Catalogo")]
    public class Catalogo
    {
        [NotMapped]
        public SGEContext context { get; set; }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal ConsumoEnergia { get; set; }
        public string IdentificadorFabrica { get; set; }

        public int? AdministradorId { get; set; } // fk con tabla administrador
        [ForeignKey("AdministradorId")]
        public virtual Administrador Administrador { get; set; }
    //    public virtual ICollection<Inteligente> Inteligentes { get; set; }

        /*   public void cargarCatalogoInicial()
           {
               BaseRepositorio<Catalogo> repocatalogo = new BaseRepositorio<Catalogo>();
               context.Catalogos.AddOrUpdate(x => x.Id,
           new Catalogo() { Id = 1, Nombre = "Aire Acondicionado", ConsumoEnergia = 1500, IdentificadorFabrica = "AA-2598", AdministradorId = 1 },
           new Catalogo() { Id = 2, Nombre = "Lavarropas", ConsumoEnergia = 895, IdentificadorFabrica = "LV-1688", AdministradorId = 1 },
           new Catalogo() { Id = 3, Nombre = "Televisor", ConsumoEnergia = 520, IdentificadorFabrica = "TV-1338", AdministradorId = 1 }
           );

           }*/
    }

}
