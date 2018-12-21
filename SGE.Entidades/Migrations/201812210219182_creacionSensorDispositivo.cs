namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creacionSensorDispositivo : DbMigration
    {
        public override void Up()
        {
            Sql("delete from condicion");
            DropForeignKey("dbo.Sensor", "UltimaMedicion_Id", "dbo.Medicion");
            DropForeignKey("dbo.Condicion", "SensorId", "dbo.Sensor");
            DropForeignKey("dbo.Medicion", "SensorId", "dbo.Sensor");
            DropIndex("dbo.Sensor", new[] { "UltimaMedicion_Id" });
            DropIndex("dbo.Medicion", new[] { "SensorId" });
            DropIndex("dbo.Medicion", new[] { "Sensor_Id" });
            DropColumn("dbo.Medicion", "SensorId");
            RenameColumn(table: "dbo.Medicion", name: "Sensor_Id", newName: "SensorId");
            CreateTable(
                "dbo.SensorFisico",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IdDispositivo = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensor", t => t.Id)
                .ForeignKey("dbo.Inteligente", t => t.IdDispositivo)
                .Index(t => t.Id)
                .Index(t => t.IdDispositivo);
            
            AlterColumn("dbo.Medicion", "FechaRegistro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Medicion", "SensorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Medicion", "SensorId");
            AddForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico", "Id");
            AddForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico", "Id");
            DropColumn("dbo.Sensor", "Discriminator");
            DropColumn("dbo.Sensor", "UltimaMedicion_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sensor", "UltimaMedicion_Id", c => c.Int());
            AddColumn("dbo.Sensor", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico");
            DropForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico");
            DropForeignKey("dbo.SensorFisico", "IdDispositivo", "dbo.Inteligente");
            DropForeignKey("dbo.SensorFisico", "Id", "dbo.Sensor");
            DropIndex("dbo.SensorFisico", new[] { "IdDispositivo" });
            DropIndex("dbo.SensorFisico", new[] { "Id" });
            DropIndex("dbo.Medicion", new[] { "SensorId" });
            AlterColumn("dbo.Medicion", "SensorId", c => c.Int());
            AlterColumn("dbo.Medicion", "FechaRegistro", c => c.DateTime(nullable: false));
            DropTable("dbo.SensorFisico");
            RenameColumn(table: "dbo.Medicion", name: "SensorId", newName: "Sensor_Id");
            AddColumn("dbo.Medicion", "SensorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Medicion", "Sensor_Id");
            CreateIndex("dbo.Medicion", "SensorId");
            CreateIndex("dbo.Sensor", "UltimaMedicion_Id");
            AddForeignKey("dbo.Medicion", "SensorId", "dbo.Sensor", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Condicion", "SensorId", "dbo.Sensor", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sensor", "UltimaMedicion_Id", "dbo.Medicion", "Id");
        }
    }
}
