using System.Data.Entity.ModelConfiguration;

namespace SGE.Entidades.Usuarios
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            //mapeo de la herencia de usuario

            HasKey(x => x.Id);
            Property(x => x.Nombre).HasMaxLength(25).IsRequired();
            Property(x => x.Apellido).HasMaxLength(25).IsRequired();
            Property(x => x.NombreUsuario).HasMaxLength(25).IsRequired();
            Property(x => x.Password).HasMaxLength(18).IsRequired();

            Map<Cliente>(x => x.Requires("Tipo_Usuario")
                                            .HasValue("CLI")
                                            .HasMaxLength(6));

            Map<Administrador>(x => x.Requires("Tipo_Usuario")
                                            .HasValue("ADMIN")
                                            .HasMaxLength(6));

        }

    }
}
