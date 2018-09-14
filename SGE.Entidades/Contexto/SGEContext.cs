using SGE.Entidades.Acciones;
using SGE.Entidades.Categorias;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Drivers;
using SGE.Entidades.Managers;
using SGE.Entidades.Reglas;
using SGE.Entidades.Transformadores;
using SGE.Entidades.Usuarios;
using SGE.Entidades.ValueProviders;
using SGE.Entidades.Zonas;
using System;
using System.Data.Entity;

namespace SGE.Entidades.Contexto
{
    public class SGEContext : DbContext
    {
        public SGEContext() : base("ConnSGEDb")
        {
          Database.SetInitializer<SGEContext>(new DropCreateDatabaseAlways<SGEContext>());
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Activacion> Activaciones { get; set; }
        public DbSet<Transformador> Transformadores { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<Sensor> Sensores { get; set; }
        public DbSet<Medicion> Mediciones { get; set; }
        public DbSet<Accion> Acciones { get; set; }
        public DbSet<Driver> Actuador { get; set; }
        public DbSet<Regla> Reglas { get; set; }
        public DbSet<Condicion> Condiciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UsuarioMap()); //mapeo herencia Usuario
            modelBuilder.Configurations.Add(new DispositivoMap()); // mapeo herencia Dispositivo
            modelBuilder.Configurations.Add(new SensorMap()); // mapeo herencia sensor
            modelBuilder.Configurations.Add(new DriverMap()); // mapeo herencia Actuador
            modelBuilder.Configurations.Add(new AccionMap()); // mapeo herencia Accion

            // mapeo relacion usuario -direccion one to one
            modelBuilder.Entity<Usuario>()
                        .HasOptional (s => s.Direccion)
                        .WithRequired(ad => ad.Usuario);

            //mapeo relacion usuario dispositivo estandar
            modelBuilder.Entity<Usuario>()
                .HasMany<Estandar>(s => s.Estandars)
                .WithMany(c => c.Usuarios)
                .Map(cs =>
                {
                    cs.MapLeftKey("EstandarId");
                    cs.MapRightKey("UsuarioId");
                    cs.ToTable("Estandar_X_Usuario");
                });

            //mapeo relacion usuario dispositivo inteligente
            modelBuilder.Entity<Usuario>()
                .HasMany<Inteligente>(s => s.Inteligentes)
                .WithMany(c => c.Usuarios)
                .Map(cs =>
                {
                    cs.MapLeftKey("InteligenteId");
                    cs.MapRightKey("UsuarioId");
                    cs.ToTable("Inteligente_X_Usuario");
                });

            // mapeo relacion cliente -tipodocumento one many to one
            modelBuilder.Entity<TipoDocumento>()
                        .HasMany<Cliente>(s => s.Clientes)
                        .WithRequired(ad => ad.TipoDocumento)
                        .HasForeignKey<int>(ad => ad.TipoDocumentoId);

            // mapeo  relacion categoria -cliente one to many
            modelBuilder.Entity<Categoria>()
                        .HasMany<Cliente>(g => g.Clientes)
                        .WithRequired(s => s.Categoria)
                        .HasForeignKey<int>(s => s.CategoriaId);

            // mapeo  relacion cliente -telefono one to many
            modelBuilder.Entity<Cliente>()
                        .HasMany<Telefono>(g => g.Telefonos)
                        .WithRequired(s => s.Cliente)
                        .HasForeignKey<int>(s => s.ClienteId);

            //mapeo relacion Inteligente - Activacion
            modelBuilder.Entity<Inteligente>()
                       .HasMany<Activacion>(g => g.RegistroDeActivaciones)
                       .WithRequired(s => s.Inteligente)
                       .HasForeignKey<int>(s => s.InteligenteId);

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

            // mapeo  relacion Sensor - Inteligente one to many
            modelBuilder.Entity<Sensor>()
                        .HasMany<Inteligente>(g => g.Inteligentes)
                        .WithRequired(s => s.Sensor)
                        .HasForeignKey<int>(s => s.SensorId);

            // mapeo relacion Sensor -Medicion one to one
            modelBuilder.Entity<Sensor>()
                        .HasMany<Medicion>(g => g.Mediciones)
                        .WithRequired(s => s.Sensor)
                        .HasForeignKey<int>(s => s.SensorId);

            //mapeo relacion regla - condicion 
            modelBuilder.Entity<Regla>()
                       .HasMany<Condicion>(g => g.Condiciones)
                       .WithRequired(s => s.Regla)
                       .HasForeignKey<int>(s => s.ReglaId);

            //mapeo relacion n:n usuario - estandar
            modelBuilder.Entity<Usuario>()
                .HasMany<Estandar>(s => s.Estandars)
                .WithMany(c => c.Usuarios)
                .Map(cs =>
                {
                    cs.MapLeftKey("EstandarId");
                    cs.MapRightKey("UsuarioId");
                    cs.ToTable("Estandar_X_Usuario");
                });

            //mapeo relacion n:n usuario - inteligente
            modelBuilder.Entity<Usuario>()
                .HasMany<Inteligente>(s => s.Inteligentes)
                .WithMany(c => c.Usuarios)
                .Map(cs =>
                {
                    cs.MapLeftKey("InteligenteId");
                    cs.MapRightKey("UsuarioId");
                    cs.ToTable("Inteligente_X_Usuario");
                });
        }
    }
}
