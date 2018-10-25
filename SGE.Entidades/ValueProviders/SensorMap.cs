using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.WebconAutenticacion.ValueProviders
{
    public class SensorMap : EntityTypeConfiguration<Sensor>
    {
        public SensorMap()
        {
            //mapeo de la herencia de sensor

            HasKey(s => s.Id);
            Property(x => x.ultimaMedicion);

            Map<SensorTemperaturaAA>(x => x.Requires("Tipo_Sensor")
                                            .HasValue("AA")
                                            .HasMaxLength(3));
        }
    }
}