using SGE.Entidades.Acciones;
using SGE.Entidades.Categorias;
using SGE.Entidades.Dispositivos;
using SGE.Entidades.Drivers;
using SGE.Entidades.Reglas;
using SGE.Entidades.Transformadores;
using SGE.Entidades.Usuarios;
using SGE.Entidades.ValueProviders;
using SGE.Entidades.Zonas;
using System.Data.Entity;

namespace SGE.Entidades.Contexto {
    public class SGEContext : DbContext
    {
        public SGEContext() : base("ConnSGEDb")
        {
            // Database.SetInitializer<SGEContext>(new DropCreateDatabaseAlways<SGEContext>());
            this.Configuration.ProxyCreationEnabled = false;
        }

        private static SGEContext Instancia = null;

        public SGEContext contextoHeredable { get; set; }

        public static SGEContext instancia() {
            if(Instancia ==null)
                Instancia = new SGEContext();

            return Instancia;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
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
        public DbSet<Inteligente> Inteligentes { get; set; }
        public DbSet<Catalogo> Catalogos { get; set; }
        public object Catalogo { get; internal set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UsuarioMap()); //mapeo herencia Usuario
            modelBuilder.Configurations.Add(new DispositivoMap()); // mapeo herencia Dispositivo
            modelBuilder.Configurations.Add(new SensorMap()); // mapeo herencia sensor
            modelBuilder.Configurations.Add(new DriverMap()); // mapeo herencia Actuador
            modelBuilder.Configurations.Add(new AccionMap()); // mapeo herencia Accion

            modelBuilder.Entity<Estandar>()
                .HasMany<Cliente>(s => s.Clientes)
                .WithMany(c => c.Estandars)
                .Map(cs =>
                {
                    cs.MapLeftKey("EstandarId");
                    cs.MapRightKey("ClienteId");
                    cs.ToTable("Estandar_X_Cliente");
                });
        }
    }
}
