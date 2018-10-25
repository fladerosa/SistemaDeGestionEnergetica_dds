using SGE.WebconAutenticacion.Acciones.AA;
using SGE.WebconAutenticacion.Acciones.Lavarropa;
using SGE.WebconAutenticacion.Acciones.TV;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.WebconAutenticacion.Acciones
{
    public class AccionMap : EntityTypeConfiguration<Accion>
    {
        public AccionMap()
        {
            //mapeo de la herencia de Accion

            HasKey(x => x.AccionId);
            Property(x => x.Descripcion);

            Map<Apagar>(x => x.Requires("Tipo_Accion")
                                            .HasValue("Apagar")
                                            .HasMaxLength(25));

            Map<Encender>(x => x.Requires("Tipo_Accion")
                                            .HasValue("Encender")
                                            .HasMaxLength(25));

            Map<ColocarEnAhorroEnergia>(x => x.Requires("Tipo_Accion")
                                            .HasValue("ModoAhorro")
                                            .HasMaxLength(25));

            Map<CambiarCanal>(x => x.Requires("Tipo_Accion")
                                            .HasValue("CambiarCanal")
                                            .HasMaxLength(25));

            Map<CambiarEntrada>(x => x.Requires("Tipo_Accion")
                                            .HasValue("CambiarEntrada")
                                            .HasMaxLength(25));

            Map<Mute>(x => x.Requires("Tipo_Accion")
                                            .HasValue("Mute")
                                            .HasMaxLength(25));

            Map<SubirVolumen>(x => x.Requires("Tipo_Accion")
                                            .HasValue("SubirVolumen")
                                            .HasMaxLength(25));

            Map<BajarVolumen>(x => x.Requires("Tipo_Accion")
                                            .HasValue("BajarVolumen")
                                            .HasMaxLength(25));

            Map<LavarRopaAlgodon>(x => x.Requires("Tipo_Accion")
                                            .HasValue("LavarAlgodon")
                                            .HasMaxLength(25));

            Map<AumentarVelocidadVentilador>(x => x.Requires("Tipo_Accion")
                                            .HasValue("AumentarVelocidad")
                                            .HasMaxLength(25));

            Map<CambiarDireccion>(x => x.Requires("Tipo_Accion")
                                            .HasValue("CambiarDireccion")
                                            .HasMaxLength(25));

            Map<DecrementarVelocidadVentilador>(x => x.Requires("Tipo_Accion")
                                            .HasValue("BajarVelocidad")
                                            .HasMaxLength(25));

            Map<EstablecerModoDry>(x => x.Requires("Tipo_Accion")
                                            .HasValue("ModoDry")
                                            .HasMaxLength(25));

            Map<EstablecerModoCool>(x => x.Requires("Tipo_Accion")
                                            .HasValue("ModoCool")
                                            .HasMaxLength(25));

            Map<EstablecerModoHeat>(x => x.Requires("Tipo_Accion")
                                            .HasValue("ModoHeat")
                                            .HasMaxLength(25));

            Map<EstablecerTemperaturaAireAcondicionado>(x => x.Requires("Tipo_Accion")
                                           .HasValue("Temperatura")
                                           .HasMaxLength(25));
        }
    }
}
