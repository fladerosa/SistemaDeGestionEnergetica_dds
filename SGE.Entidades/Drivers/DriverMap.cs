using System.Data.Entity.ModelConfiguration;

namespace SGE.Entidades.Drivers {
    public class DriverMap : EntityTypeConfiguration<Driver>
    {
        public DriverMap()
        {
            //mapeo de la herencia de Actuador

            HasKey(x => x.Id);
            Property(x => x.Mensaje);

            Map<DreanLavarropasDriver>(x => x.Requires("Tipo_Actuador")
                                            .HasValue("TipoLavado")
                                            .HasMaxLength(15));
            Map<SamsungAireAcondicionadoDriver>(x => x.Requires("Tipo_Actuador")
                                            .HasValue("Temperatura")
                                            .HasMaxLength(15));

            Map<SonyTVDriver>(x => x.Requires("Tipo_Actuador")
                                            .HasValue("Volumen")
                                            .HasMaxLength(15));
        }
    }
}
