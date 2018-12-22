namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambio5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico");
            DropForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico");
            DropTable("dbo.SensorFisico");
            CreateTable(
                "dbo.SensorFisico",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    IdDispositivo = c.Int(nullable: false),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensor", t => t.Id)
                .ForeignKey("dbo.Inteligente", t => t.IdDispositivo)
                .Index(t => t.Id)
                .Index(t => t.IdDispositivo);
            AddForeignKey("dbo.Medicion", "SensorId", "dbo.SensorFisico", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Condicion", "SensorId", "dbo.SensorFisico", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
        }
    }
}
