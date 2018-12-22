namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class normalizacionSensores1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico");
            DropForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico");
            DropForeignKey("dbo.SensorFisico", "Id", "dbo.Sensor");
            DropIndex("dbo.SensorFisico", new[] { "Id" });
            DropPrimaryKey("dbo.SensorFisico");
            AddColumn("dbo.SensorFisico", "IdTipoSensor", c => c.Int(nullable: false));
            AddColumn("dbo.Sensor", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.SensorFisico", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.SensorFisico", "Id");
            CreateIndex("dbo.SensorFisico", "IdTipoSensor");
            AddForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SensorFisico", "IdTipoSensor", "dbo.Sensor", "Id", cascadeDelete: true);
            DropColumn("dbo.SensorFisico", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SensorFisico", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.SensorFisico", "IdTipoSensor", "dbo.Sensor");
            DropForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico");
            DropForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico");
            DropIndex("dbo.SensorFisico", new[] { "IdTipoSensor" });
            DropPrimaryKey("dbo.SensorFisico");
            AlterColumn("dbo.SensorFisico", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Sensor", "Discriminator");
            DropColumn("dbo.SensorFisico", "IdTipoSensor");
            AddPrimaryKey("dbo.SensorFisico", "Id");
            CreateIndex("dbo.SensorFisico", "Id");
            AddForeignKey("dbo.SensorFisico", "Id", "dbo.Sensor", "Id");
            AddForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico", "Id");
            AddForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico", "Id");
        }
    }
}
