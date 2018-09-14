using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Drivers
{
    public class DriverMap : EntityTypeConfiguration<Driver>
    {
        public DriverMap()
        {
            //mapeo de la herencia de Actuador

            HasKey(x => x.ActuadorId);
            Property(x => x.Mensaje);

            Map<DreanLavarropasDriver>(x => x.Requires("Tipo_Actuador")
                                            .HasValue("Lav")
                                            .HasMaxLength(3));
            Map<SamsungAireAcondicionadoDriver>(x => x.Requires("Tipo_Actuador")
                                            .HasValue("Aco")
                                            .HasMaxLength(3));

            Map<SonyTVDriver>(x => x.Requires("Tipo_Actuador")
                                            .HasValue("TV")
                                            .HasMaxLength(3));
        }
    }
}
