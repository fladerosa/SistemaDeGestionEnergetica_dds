namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class normalizacionCatalogo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SensorCatalogoes",
                c => new
                    {
                        Sensor_Id = c.Int(nullable: false),
                        Catalogo_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sensor_Id, t.Catalogo_Id })
                .ForeignKey("dbo.Sensor", t => t.Sensor_Id, cascadeDelete: true)
                .ForeignKey("dbo.Catalogo", t => t.Catalogo_Id, cascadeDelete: true)
                .Index(t => t.Sensor_Id)
                .Index(t => t.Catalogo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorCatalogoes", "Catalogo_Id", "dbo.Catalogo");
            DropForeignKey("dbo.SensorCatalogoes", "Sensor_Id", "dbo.Sensor");
            DropIndex("dbo.SensorCatalogoes", new[] { "Catalogo_Id" });
            DropIndex("dbo.SensorCatalogoes", new[] { "Sensor_Id" });
            DropTable("dbo.SensorCatalogoes");
        }
    }
}
