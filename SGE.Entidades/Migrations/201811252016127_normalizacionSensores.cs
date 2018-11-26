namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class normalizacionSensores : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Medicion", "SensorId", "dbo.Sensor");
            RenameColumn(table: "dbo.Medicion", name: "CondicionId", newName: "Condicion_CondicionId");
            RenameIndex(table: "dbo.Medicion", name: "IX_CondicionId", newName: "IX_Condicion_CondicionId");
            DropPrimaryKey("dbo.Medicion");
            DropColumn("dbo.Medicion", "MedicionId");
            DropColumn("dbo.Sensor", "UltimaMedicion");
            AddColumn("dbo.Condicion", "Sensor_Id", c => c.Int());
            AddColumn("dbo.Medicion", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Medicion", "Sensor_Id", c => c.Int());
            AddColumn("dbo.Sensor", "Descripcion", c => c.String());
            AddColumn("dbo.Sensor", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Sensor", "UltimaMedicion_Id", c => c.Int());
            AlterColumn("dbo.Medicion", "Valor", c => c.String());
            AlterColumn("dbo.Medicion", "FechaRegistro", c => c.DateTime(nullable: false));
            AddPrimaryKey("dbo.Medicion", "Id");
            CreateIndex("dbo.Condicion", "Sensor_Id");
            CreateIndex("dbo.Medicion", "Sensor_Id");
            CreateIndex("dbo.Sensor", "UltimaMedicion_Id");
            AddForeignKey("dbo.Condicion", "Sensor_Id", "dbo.Sensor", "Id");
            AddForeignKey("dbo.Sensor", "UltimaMedicion_Id", "dbo.Medicion", "Id");
            AddForeignKey("dbo.Medicion", "Sensor_Id", "dbo.Sensor", "Id");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sensor", "UltimaMedicion", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Medicion", "MedicionId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Medicion", "Sensor_Id", "dbo.Sensor");
            DropForeignKey("dbo.Sensor", "UltimaMedicion_Id", "dbo.Medicion");
            DropForeignKey("dbo.Condicion", "Sensor_Id", "dbo.Sensor");
            DropIndex("dbo.Sensor", new[] { "UltimaMedicion_Id" });
            DropIndex("dbo.Medicion", new[] { "Sensor_Id" });
            DropIndex("dbo.Condicion", new[] { "Sensor_Id" });
            DropPrimaryKey("dbo.Medicion");
            AlterColumn("dbo.Medicion", "FechaRegistro", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Medicion", "Valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Sensor", "UltimaMedicion_Id");
            DropColumn("dbo.Sensor", "Discriminator");
            DropColumn("dbo.Sensor", "Descripcion");
            DropColumn("dbo.Medicion", "Sensor_Id");
            DropColumn("dbo.Medicion", "Id");
            DropColumn("dbo.Condicion", "Sensor_Id");
            AddPrimaryKey("dbo.Medicion", "MedicionId");
            RenameIndex(table: "dbo.Medicion", name: "IX_Condicion_CondicionId", newName: "IX_CondicionId");
            RenameColumn(table: "dbo.Medicion", name: "Condicion_CondicionId", newName: "CondicionId");
            AddForeignKey("dbo.Medicion", "SensorId", "dbo.Sensor", "Id", cascadeDelete: true);
        }
    }
}
