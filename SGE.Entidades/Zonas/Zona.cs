using SGE.Core.Helpers;
using SGE.Entidades.Repositorio;
using SGE.Entidades.Transformadores;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGE.Entidades.Zonas {
    [Table("Zona")]
    public class Zona
    {
        #region Propiedades
        public int Id { get; set; }
        public int codigo { get; set; }
        [MaxLength(15)]
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public decimal Radio { get; set; }

        public virtual ICollection<Transformador> Transformadores { get; set; } //one to many con Transformador
   
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

        public void ProcesarDatosEnre() {
            ZonasHelper zonaHelper = new ZonasHelper();
            BaseRepositorio<Zona> repoZona = new BaseRepositorio<Zona>();

            foreach (Core.Entidades.Zona zona in zonaHelper.Zonas) {
                if(repoZona.Single(z => z.codigo == zona.codigo) == null)
                    repoZona.Create(new Zona() {
                        Id = zona.Id,
                        codigo = zona.codigo,
                        Nombre = zona.Nombre,
                        Latitud = (double)zona.Latitud,
                        Longitud = (double)zona.Longitud,
                        Radio = zona.Radio
                    });
            }
        }

        #endregion
    }
}
