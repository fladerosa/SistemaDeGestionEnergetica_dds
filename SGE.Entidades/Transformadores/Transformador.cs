using SGE.Core.Helpers;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Usuarios;
using SGE.Entidades.Zonas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SGE.Entidades.Transformadores {
    [Table("Transformador")]
    public class Transformador
    {
        #region Propiedades
        public int Id { get; set; }
        public int codigo { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public int ZonaId { get; set; } //fk con tabla Zona
        [ForeignKey("ZonaId")]
        public virtual Zona Zona { get; set; } // one to many con  Zona

        public Transformador()
        {
            this.Clientes = new List<Cliente>();
        }

        public decimal ObtenerConsumo()
        {
            decimal consumo = 0;
            foreach (Cliente cliente in Clientes)
            {
                consumo += cliente.Estandars.Sum(e => e.ConsumoEnergia);
                consumo += cliente.Inteligentes.Sum(i => i.ConsumoEnergia);
            }
            return consumo;
        }

        public void ProcesarDatosEnre() {
            BaseRepositorio<Transformador> repoTransformador = new BaseRepositorio<Transformador>();
            BaseRepositorio<Zona> repoZona = new BaseRepositorio<Zona>();
            TransformadoresHelper transHelper = new TransformadoresHelper();

            foreach (Core.Entidades.Transformador transformador in transHelper.Transformadores) {
                if(repoTransformador.Single(t => t.codigo == transformador.codigo) == null)
                    repoTransformador.Create(new Transformador() {
                        codigo = transformador.codigo,
                        Latitud = (double)transformador.Latitud,
                        Longitud = (double)transformador.Longitud,
                        ZonaId = repoZona.Single(z => z.codigo == transformador.Zona).Id
                    });
            }
        }
        #endregion
    }
}
