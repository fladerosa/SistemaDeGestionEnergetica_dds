namespace SGE.Entidades.Migrations {
    using SGE.Entidades.Acciones.Acciones;
    using SGE.Entidades.Reglas.Operadores;
    using SGE.Entidades.Sensores.Sensores;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SGE.Entidades.Contexto.SGEContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SGE.Entidades.Contexto.SGEContext";
        }

        protected override void Seed(SGE.Entidades.Contexto.SGEContext context) {
            context.Acciones.AddOrUpdate(a => a.Descripcion,
                new Apagar() { Descripcion = "Apaga el dispositivo" },
                new AumentarVelocidadVentilador() { Descripcion = "Incremente la velocidad del ventilador" },
                new BajarVolumen() { Descripcion = "Baja el volumen según lo indicado" },
                new ColocarEnAhorroEnergia() { Descripcion = "Inicia el ahorro de energía" },
                new DecrementarVelocidadVentilador() { Descripcion = "Disminuye la velocidad del ventilador" },
                new Encender() { Descripcion = "Enciende el dispositivo" },
                new EstablecerModoCalor() { Descripcion = "Inicia el modo calor" },
                new EstablecerModoFrio() { Descripcion = "Inicia el modo frío" },
                new Mute() { Descripcion = "Silencio" },
                new SubirVolumen() { Descripcion = "Incrementa el volumen según lo indicado" }
            );

            context.Sensores.AddOrUpdate(s => s.Descripcion,
                new Estado() { Descripcion = "Estado" },
                new Volumen() { Descripcion = "Volumen" },
                new Temperatura() { Descripcion = "Temperatura" },
                new EstadoInterno() { Descripcion = "Estado Interno" },
                new VelocidadVentilador() { Descripcion = "Velocidad Ventilador" }
            );

            context.Operadores.AddOrUpdate(o => o.Descripcion,
                new Igual() { Descripcion = "Igual" },
                new Mayor() { Descripcion = "Mayor" },
                new MayorOIgual() { Descripcion = "Mayor O Igual" },
                new Menor() { Descripcion = "Menor" },
                new MenorOIgual() { Descripcion = "Menor O Igual" }
            );
        }
    }
}
