using System.Data.Entity.ModelConfiguration;

namespace SGE.WebconAutenticacion.Dispositivos
{
    public class DispositivoMap : EntityTypeConfiguration<Dispositivo>
    {
        public DispositivoMap()
        {
            //mapeo de la herencia de dispositivo

            HasKey(x => x.Id);
            Property(x => x.Nombre).HasMaxLength(25).IsRequired();
            Property(x => x.ConsumoEnergia).IsRequired();
            Property(x => x.IdentificadorFabrica).HasMaxLength(50);

            Map<Estandar>(x => x.Requires("Tipo_Dispositivo")
                                            .HasValue("E")
                                            .HasMaxLength(3));

            Map<Inteligente>(x => x.Requires("Tipo_Dispositivo")
                                           .HasValue("I")
                                           .HasMaxLength(3));

        }
    }
}
