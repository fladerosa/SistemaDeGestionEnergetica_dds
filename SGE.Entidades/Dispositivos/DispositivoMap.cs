using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Dispositivos
{
    public class DispositivoMap : EntityTypeConfiguration<Dispositivo>
    {
        public DispositivoMap()
        {
            //mapeo de la herencia de dispositivo

            HasKey(x => x.Id);
            Property(x => x.Nombre).HasMaxLength(25).IsRequired();
            Property(x => x.ConsumoEnergia).IsRequired();

            Property(x => x.IdentificadorFabrica).HasMaxLength(15).IsRequired();

            Map<Estandar>(x => x.Requires("Tipo_Dispositivo")
                                            .HasValue("E")
                                            .HasColumnType("char")
                                            .HasMaxLength(1));

            Map<Inteligente>(x => x.Requires("Tipo_Dispositivo")
                                            .HasValue("I"));

        }
    }
}
