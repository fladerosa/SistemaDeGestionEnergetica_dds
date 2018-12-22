namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambio1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico");
            DropForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico");
            DropPrimaryKey("dbo.SensorFisico");
            AlterColumn("dbo.SensorFisico", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.SensorFisico", "Id");
            AddForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico");
            DropForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico");
            DropPrimaryKey("dbo.SensorFisico");
            AlterColumn("dbo.SensorFisico", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.SensorFisico", "Id");
            AddForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico", "Id", cascadeDelete: true);
        }
    }
}
