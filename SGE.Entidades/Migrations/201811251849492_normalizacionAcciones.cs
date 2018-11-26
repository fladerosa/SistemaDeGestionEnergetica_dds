namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class normalizacionAcciones : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ClienteInteligentes", newName: "InteligenteClientes");
            DropForeignKey("dbo.Accion", "ActuadorId", "dbo.Actuador");
            DropForeignKey("dbo.Accion", "ReglaId", "dbo.Regla");
            DropForeignKey("dbo.Inteligente", "ActuadorId", "dbo.Actuador");
            DropIndex("dbo.Accion", new[] { "ReglaId" });
            DropIndex("dbo.Accion", new[] { "ActuadorId" });
            DropIndex("dbo.Inteligente", new[] { "ActuadorId" });
            RenameColumn(table: "dbo.Accion", name: "Tipo_Accion", newName: "Discriminator");
            DropPrimaryKey("dbo.Accion");
            DropPrimaryKey("dbo.InteligenteClientes");
            CreateTable(
                "dbo.CatalogoAccions",
                c => new
                    {
                        Catalogo_Id = c.Int(nullable: false),
                        Accion_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Catalogo_Id, t.Accion_Id })
                .ForeignKey("dbo.Catalogo", t => t.Catalogo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accion", t => t.Accion_Id, cascadeDelete: true)
                .Index(t => t.Catalogo_Id)
                .Index(t => t.Accion_Id);
            
            CreateTable(
                "dbo.ReglaAccions",
                c => new
                    {
                        Regla_ReglaId = c.Int(nullable: false),
                        Accion_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Regla_ReglaId, t.Accion_Id })
                .ForeignKey("dbo.Regla", t => t.Regla_ReglaId, cascadeDelete: true)
                .ForeignKey("dbo.Accion", t => t.Accion_Id, cascadeDelete: true)
                .Index(t => t.Regla_ReglaId)
                .Index(t => t.Accion_Id);

            DropColumn("dbo.Accion", "AccionId");
            DropColumn("dbo.Accion", "ReglaId");
            DropColumn("dbo.Accion", "ActuadorId");
            DropColumn("dbo.Inteligente", "ActuadorId");
            DropColumn("dbo.Sensor", "Tipo_Sensor");
            DropTable("dbo.Actuador");
            AddColumn("dbo.Accion", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Dispositivo", "Volumen", c => c.Int(nullable: false));
            AddColumn("dbo.Dispositivo", "Temperatura", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Dispositivo", "EstadoInterno", c => c.Int(nullable: false));
            AddColumn("dbo.Dispositivo", "VelocidadVentilador", c => c.Int(nullable: false));
            AddColumn("dbo.Regla", "IdInteligente", c => c.Int(nullable: false));
            AlterColumn("dbo.Accion", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Accion", "Id");
            AddPrimaryKey("dbo.InteligenteClientes", new[] { "Inteligente_Id", "Cliente_Id" });
            CreateIndex("dbo.Regla", "IdInteligente");
            AddForeignKey("dbo.Regla", "IdInteligente", "dbo.Inteligente", "Id");
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Actuador",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mensaje = c.String(),
                        Tipo_Actuador = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sensor", "Tipo_Sensor", c => c.String(nullable: false, maxLength: 3));
            AddColumn("dbo.Inteligente", "ActuadorId", c => c.Int());
            AddColumn("dbo.Accion", "ActuadorId", c => c.Int(nullable: false));
            AddColumn("dbo.Accion", "ReglaId", c => c.Int(nullable: false));
            AddColumn("dbo.Accion", "AccionId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Regla", "IdInteligente", "dbo.Inteligente");
            DropForeignKey("dbo.ReglaAccions", "Accion_Id", "dbo.Accion");
            DropForeignKey("dbo.ReglaAccions", "Regla_ReglaId", "dbo.Regla");
            DropForeignKey("dbo.CatalogoAccions", "Accion_Id", "dbo.Accion");
            DropForeignKey("dbo.CatalogoAccions", "Catalogo_Id", "dbo.Catalogo");
            DropIndex("dbo.ReglaAccions", new[] { "Accion_Id" });
            DropIndex("dbo.ReglaAccions", new[] { "Regla_ReglaId" });
            DropIndex("dbo.CatalogoAccions", new[] { "Accion_Id" });
            DropIndex("dbo.CatalogoAccions", new[] { "Catalogo_Id" });
            DropIndex("dbo.Regla", new[] { "IdInteligente" });
            DropPrimaryKey("dbo.InteligenteClientes");
            DropPrimaryKey("dbo.Accion");
            AlterColumn("dbo.Accion", "Discriminator", c => c.String(nullable: false, maxLength: 25));
            DropColumn("dbo.Regla", "IdInteligente");
            DropColumn("dbo.Dispositivo", "VelocidadVentilador");
            DropColumn("dbo.Dispositivo", "EstadoInterno");
            DropColumn("dbo.Dispositivo", "Temperatura");
            DropColumn("dbo.Dispositivo", "Volumen");
            DropColumn("dbo.Accion", "Id");
            DropTable("dbo.ReglaAccions");
            DropTable("dbo.CatalogoAccions");
            AddPrimaryKey("dbo.InteligenteClientes", new[] { "Cliente_Id", "Inteligente_Id" });
            AddPrimaryKey("dbo.Accion", "AccionId");
            RenameColumn(table: "dbo.Accion", name: "Discriminator", newName: "Tipo_Accion");
            CreateIndex("dbo.Inteligente", "ActuadorId");
            CreateIndex("dbo.Accion", "ActuadorId");
            CreateIndex("dbo.Accion", "ReglaId");
            AddForeignKey("dbo.Inteligente", "ActuadorId", "dbo.Actuador", "Id");
            AddForeignKey("dbo.Accion", "ReglaId", "dbo.Regla", "ReglaId", cascadeDelete: true);
            AddForeignKey("dbo.Accion", "ActuadorId", "dbo.Actuador", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.InteligenteClientes", newName: "ClienteInteligentes");
        }
    }
}
