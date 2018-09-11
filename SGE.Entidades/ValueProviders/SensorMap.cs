using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.ValueProviders
{
    public class SensorMap : EntityTypeConfiguration<Sensor>
    {
        public SensorMap()
        {
            //mapeo de la herencia de dispositivo
            HasKey(x => x.SensorId);
          
            Map<SensorTemperaturaAA>(x => x.Requires("Tipo_Sensor")
                                            .HasValue("Temperatura")
                                            .HasMaxLength(15));
        }
    }