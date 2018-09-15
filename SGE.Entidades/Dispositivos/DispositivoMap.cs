using System.Data.Entity.ModelConfiguration;

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

            Property(x => x.IdentificadorFabrica).HasMaxLength(15);

            Map<Estandar>(x => x.Requires("Tipo_Dispositivo")
                                            .HasValue("E")
                                            .HasMaxLength(3));

            Map<Inteligente>(x => x.Requires("Tipo_Dispositivo")
                                            .HasValue("I")
                                            .HasMaxLength(3));

            //Map<AireAcondicionado>(x => x.Requires("Tipo_Inteligente")
            //                                .HasValue("AA")
            //                                .HasMaxLength(3));

            //Map<Lavarropas>(x => x.Requires("Tipo_Inteligente")
            //                                .HasValue("LV")
            //                                .HasMaxLength(3));

            //Map<Televisor>(x => x.Requires("Tipo_Inteligente")
            //                                .HasValue("TV")
            //                                .HasMaxLength(3));
        }
    }
}
