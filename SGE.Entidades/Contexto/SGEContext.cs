using SGE.Entidades.Categorias;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Usuarios;
using System;
using System.Data.Entity;

namespace SGE.Entidades.Contexto
{
    public class SGEContext : DbContext
    {
        public SGEContext() : base("ConnSGEDb")
        {
            Database.SetInitializer<SGEContext>(new DropCreateDatabaseAlways<SGEContext>());

            //Database.SetInitializer<SGEContext>(new CreateDatabaseIfNotExists<SGEContext>());

            /*  this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            */

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Inteligente> Inteligentes { get; set; }
        public DbSet<Estandar> Estandars { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Activacion> Activaciones { get; set; }
        public DbSet<Transformador> Transformadores { get; set; }
        public DbSet<Zona> Zonas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UsuarioMap()); //mapeo herencia Usuario
            modelBuilder.Configurations.Add(new DispositivoMap()); // mapeo herencia Dispositivo

            // mapeo relacion usuario -direccion one to one
            modelBuilder.Entity<Usuario>()
                        .HasRequired(s => s.Direccion)
                        .WithRequiredPrincipal(ad => ad.Usuario);


            // mapeo relacion cliente -tipodocumento one to one
            modelBuilder.Entity<Cliente>()
                        .HasRequired(s => s.TipoDocumento)
                        .WithRequiredPrincipal(ad => ad.Cliente);

            // mapeo  relacion categoria -cliente one to many
            modelBuilder.Entity<Categoria>()
                        .HasMany<Cliente>((Categoria g) => g.Clientes)
                        .WithRequired((System.Linq.Expressions.Expression<Func<Cliente, Categoria>>)(s => s.Categoria))
                        .HasForeignKey<int>(s => s.CategoriaId);

            // mapeo  relacion cliente -telefono one to many
            modelBuilder.Entity<Cliente>()
                        .HasMany<Telefono>(g => g.Telefonos)
                        .WithRequired(s => s.Cliente)
                        .HasForeignKey<int>(s => s.ClienteId);

            //mapeo relacion Inteligente - Activacion
            modelBuilder.Entity<Inteligente>()
                       .HasMany<Activacion>(g => g.Activaciones)
                       .WithRequired(s => s.Inteligente)
                       .HasForeignKey<int>(s => s.InteligenteId);

            //mapeo Relacion zona - direccion
            modelBuilder.Entity<Zona>()
                .HasOptional(s => s.Direccion)
                .WithRequired(ad => ad.Zona);

            //mapeo Relacion Zona - Transformador
            modelBuilder.Entity<Zona>()
                         .HasMany<Transformador>(g => g.Transformadores)
                         .WithRequired(s => s.Zona)
                         .HasForeignKey<int>(s => s.ZonaId);

            //mapeo Transformador - Cliente
            modelBuilder.Entity<Transformador>()
                        .HasMany<Cliente>(g => g.Clientes)
                        .WithRequired(s => s.Transformador)
                        .HasForeignKey<int>(s => s.TransformadorId);

        }
    }
}
