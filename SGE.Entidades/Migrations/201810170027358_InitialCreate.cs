namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accion",
                c => new
                    {
                        AccionId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        ReglaId = c.Int(nullable: false),
                        ActuadorId = c.Int(nullable: false),
                        Tipo_Accion = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.AccionId)
                .ForeignKey("dbo.Actuador", t => t.ActuadorId, cascadeDelete: true)
                .ForeignKey("dbo.Regla", t => t.ReglaId, cascadeDelete: true)
                .Index(t => t.ReglaId)
                .Index(t => t.ActuadorId);
            
            CreateTable(
                "dbo.Actuador",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mensaje = c.String(),
                        Tipo_Actuador = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dispositivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25),
                        ConsumoEnergia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdentificadorFabrica = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25),
                        Apellido = c.String(nullable: false, maxLength: 25),
                        NombreUsuario = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false, maxLength: 60),
                        FechaAlta = c.DateTime(nullable: false),
                        MensajeDeErrorLogueo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        ConsumoMinimo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumoMaximo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostoFijo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostoVariable = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Direccion",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Calle = c.String(maxLength: 25),
                        Nro = c.String(maxLength: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Activacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Estado = c.Int(nullable: false),
                        FechaDeRegistro = c.DateTime(nullable: false),
                        InteligenteId = c.Int(nullable: false),
                        ClienteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .ForeignKey("dbo.Inteligente", t => t.InteligenteId)
                .Index(t => t.InteligenteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.String(maxLength: 16),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Transformador",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        codigo = c.Int(nullable: false),
                        Latitud = c.Double(nullable: false),
                        Longitud = c.Double(nullable: false),
                        ZonaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Zona", t => t.ZonaId, cascadeDelete: true)
                .Index(t => t.ZonaId);
            
            CreateTable(
                "dbo.Zona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        codigo = c.Int(nullable: false),
                        Nombre = c.String(maxLength: 15),
                        Latitud = c.Double(nullable: false),
                        Longitud = c.Double(nullable: false),
                        Radio = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sensor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ultimaMedicion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tipo_Sensor = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicion",
                c => new
                    {
                        MedicionId = c.Int(nullable: false, identity: true),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unidad = c.Int(nullable: false),
                        FechaRegistro = c.DateTime(nullable: false),
                        SensorId = c.Int(nullable: false),
                        CondicionId = c.Int(),
                    })
                .PrimaryKey(t => t.MedicionId)
                .ForeignKey("dbo.Condicion", t => t.CondicionId)
                .ForeignKey("dbo.Sensor", t => t.SensorId, cascadeDelete: true)
                .Index(t => t.SensorId)
                .Index(t => t.CondicionId);
            
            CreateTable(
                "dbo.Condicion",
                c => new
                    {
                        CondicionId = c.Int(nullable: false, identity: true),
                        valorReferencia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReglaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CondicionId)
                .ForeignKey("dbo.Regla", t => t.ReglaId, cascadeDelete: true)
                .Index(t => t.ReglaId);
            
            CreateTable(
                "dbo.Regla",
                c => new
                    {
                        ReglaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.ReglaId);
            
            CreateTable(
                "dbo.Estandar_X_Cliente",
                c => new
                    {
                        EstandarId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EstandarId, t.ClienteId })
                .ForeignKey("dbo.Estandar", t => t.EstandarId, cascadeDelete: true)
                .ForeignKey("dbo.Cliente", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.EstandarId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Inteligente_X_Cliente",
                c => new
                    {
                        InteligenteId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InteligenteId, t.ClienteId })
                .ForeignKey("dbo.Inteligente", t => t.InteligenteId, cascadeDelete: true)
                .ForeignKey("dbo.Cliente", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.InteligenteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Inteligente",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SensorId = c.Int(),
                        ActuadorId = c.Int(),
                        Tipo_Dispositivo = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dispositivo", t => t.Id)
                .ForeignKey("dbo.Sensor", t => t.SensorId)
                .ForeignKey("dbo.Actuador", t => t.ActuadorId)
                .Index(t => t.Id)
                .Index(t => t.SensorId)
                .Index(t => t.ActuadorId);
            
            CreateTable(
                "dbo.Estandar",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Tipo_Dispositivo = c.String(nullable: false, maxLength: 3),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dispositivo", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NumeroDocumento = c.String(),
                        Latitud = c.Double(nullable: false),
                        Longitud = c.Double(nullable: false),
                        TransformadorId = c.Int(nullable: false),
                        TipoDocumento = c.Int(nullable: false),
                        CategoriaId = c.Int(nullable: false),
                        Tipo_Usuario = c.String(nullable: false, maxLength: 6),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Id)
                .ForeignKey("dbo.Transformador", t => t.TransformadorId, cascadeDelete: true)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TransformadorId)
                .Index(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Administrador",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Nui = c.String(),
                        Tipo_Usuario = c.String(nullable: false, maxLength: 6),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Administrador", "Id", "dbo.Usuario");
            DropForeignKey("dbo.Cliente", "CategoriaId", "dbo.Categoria");
            DropForeignKey("dbo.Cliente", "TransformadorId", "dbo.Transformador");
            DropForeignKey("dbo.Cliente", "Id", "dbo.Usuario");
            DropForeignKey("dbo.Estandar", "Id", "dbo.Dispositivo");
            DropForeignKey("dbo.Inteligente", "ActuadorId", "dbo.Actuador");
            DropForeignKey("dbo.Inteligente", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.Inteligente", "Id", "dbo.Dispositivo");
            DropForeignKey("dbo.Medicion", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.Condicion", "ReglaId", "dbo.Regla");
            DropForeignKey("dbo.Accion", "ReglaId", "dbo.Regla");
            DropForeignKey("dbo.Medicion", "CondicionId", "dbo.Condicion");
            DropForeignKey("dbo.Inteligente_X_Cliente", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Inteligente_X_Cliente", "InteligenteId", "dbo.Inteligente");
            DropForeignKey("dbo.Transformador", "ZonaId", "dbo.Zona");
            DropForeignKey("dbo.Telefono", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Activacion", "InteligenteId", "dbo.Inteligente");
            DropForeignKey("dbo.Activacion", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Estandar_X_Cliente", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Estandar_X_Cliente", "EstandarId", "dbo.Estandar");
            DropForeignKey("dbo.Direccion", "Id", "dbo.Usuario");
            DropForeignKey("dbo.Accion", "ActuadorId", "dbo.Actuador");
            DropIndex("dbo.Administrador", new[] { "Id" });
            DropIndex("dbo.Cliente", new[] { "CategoriaId" });
            DropIndex("dbo.Cliente", new[] { "TransformadorId" });
            DropIndex("dbo.Cliente", new[] { "Id" });
            DropIndex("dbo.Estandar", new[] { "Id" });
            DropIndex("dbo.Inteligente", new[] { "ActuadorId" });
            DropIndex("dbo.Inteligente", new[] { "SensorId" });
            DropIndex("dbo.Inteligente", new[] { "Id" });
            DropIndex("dbo.Inteligente_X_Cliente", new[] { "ClienteId" });
            DropIndex("dbo.Inteligente_X_Cliente", new[] { "InteligenteId" });
            DropIndex("dbo.Estandar_X_Cliente", new[] { "ClienteId" });
            DropIndex("dbo.Estandar_X_Cliente", new[] { "EstandarId" });
            DropIndex("dbo.Condicion", new[] { "ReglaId" });
            DropIndex("dbo.Medicion", new[] { "CondicionId" });
            DropIndex("dbo.Medicion", new[] { "SensorId" });
            DropIndex("dbo.Transformador", new[] { "ZonaId" });
            DropIndex("dbo.Telefono", new[] { "ClienteId" });
            DropIndex("dbo.Activacion", new[] { "ClienteId" });
            DropIndex("dbo.Activacion", new[] { "InteligenteId" });
            DropIndex("dbo.Direccion", new[] { "Id" });
            DropIndex("dbo.Accion", new[] { "ActuadorId" });
            DropIndex("dbo.Accion", new[] { "ReglaId" });
            DropTable("dbo.Administrador");
            DropTable("dbo.Cliente");
            DropTable("dbo.Estandar");
            DropTable("dbo.Inteligente");
            DropTable("dbo.Inteligente_X_Cliente");
            DropTable("dbo.Estandar_X_Cliente");
            DropTable("dbo.Regla");
            DropTable("dbo.Condicion");
            DropTable("dbo.Medicion");
            DropTable("dbo.Sensor");
            DropTable("dbo.Zona");
            DropTable("dbo.Transformador");
            DropTable("dbo.Telefono");
            DropTable("dbo.Activacion");
            DropTable("dbo.Direccion");
            DropTable("dbo.Categoria");
            DropTable("dbo.Usuario");
            DropTable("dbo.Dispositivo");
            DropTable("dbo.Actuador");
            DropTable("dbo.Accion");
        }
    }
}
