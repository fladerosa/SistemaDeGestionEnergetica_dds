using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entidades.Usuarios
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            //mapeo de la herencia de usuario

            HasKey(x => x.Id);
            Property(x => x.NumeroDocumento).HasMaxLength(25).IsRequired();
            Property(x => x.Apellido).HasMaxLength(25).IsRequired();
            Property(x => x.NombreUsuario).HasMaxLength(10).IsRequired();
            Property(x => x.Password).HasMaxLength(8).IsRequired();

            Property(x => x.NumeroDocumento).HasMaxLength(10);

            Map<Cliente>(x => x.Requires("Tipo_Usuario")
                                            .HasValue("CLI")
                                            .HasColumnType("char")
                                            .HasMaxLength(6));

            Map<Administrador>(x => x.Requires("Tipo_Usuario")
                                            .HasValue("ADMIN"));

        }

    }
}
