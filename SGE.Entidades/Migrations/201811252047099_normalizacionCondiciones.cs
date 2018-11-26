namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class normalizacionCondiciones : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Condicion", new[] { "Sensor_Id" });
            DropIndex("dbo.Medicion", new[] { "Condicion_CondicionId" });
            DropForeignKey("dbo.Medicion", "CondicionId", "dbo.Condicion");
            DropForeignKey("dbo.Condicion", "Sensor_Id", "dbo.Sensor");
            DropColumn("dbo.Condicion", "TipoOperacion");
            DropColumn("dbo.Medicion", "Condicion_CondicionId");

            RenameColumn(table: "dbo.Condicion", name: "Sensor_Id", newName: "SensorId");
            CreateTable(
                "dbo.Operadores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Condicion", "Operador_Id", c => c.Int());
            AlterColumn("dbo.Condicion", "ValorReferencia", c => c.String());
            AlterColumn("dbo.Condicion", "SensorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Condicion", "SensorId");
            CreateIndex("dbo.Condicion", "Operador_Id");
            AddForeignKey("dbo.Condicion", "Operador_Id", "dbo.Operadores", "Id");
            AddForeignKey("dbo.Condicion", "SensorId", "dbo.Sensor", "Id", cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medicion", "Condicion_CondicionId", c => c.Int());
            AddColumn("dbo.Condicion", "TipoOperacion", c => c.Int(nullable: false));
            DropForeignKey("dbo.Condicion", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.Condicion", "Operador_Id", "dbo.Operadores");
            DropIndex("dbo.Condicion", new[] { "Operador_Id" });
            DropIndex("dbo.Condicion", new[] { "SensorId" });
            AlterColumn("dbo.Condicion", "SensorId", c => c.Int());
            AlterColumn("dbo.Condicion", "ValorReferencia", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Condicion", "Operador_Id");
            DropTable("dbo.Operadores");
            RenameColumn(table: "dbo.Condicion", name: "SensorId", newName: "Sensor_Id");
            CreateIndex("dbo.Medicion", "Condicion_CondicionId");
            CreateIndex("dbo.Condicion", "Sensor_Id");
            AddForeignKey("dbo.Condicion", "Sensor_Id", "dbo.Sensor", "Id");
            AddForeignKey("dbo.Medicion", "Condicion_CondicionId", "dbo.Condicion", "CondicionId");
        }
    }
}
