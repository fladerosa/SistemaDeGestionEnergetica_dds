namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class normalizacionInteligente : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Inteligente", "SensorId", "dbo.Sensor");
            DropIndex("dbo.Inteligente", new[] { "SensorId" });
            AddColumn("dbo.Inteligente", "CatalogoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Inteligente", "CatalogoId");
            AddForeignKey("dbo.Inteligente", "CatalogoId", "dbo.Catalogo", "Id", cascadeDelete: true);
            DropColumn("dbo.Inteligente", "SensorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inteligente", "SensorId", c => c.Int());
            DropForeignKey("dbo.Inteligente", "CatalogoId", "dbo.Catalogo");
            DropIndex("dbo.Inteligente", new[] { "CatalogoId" });
            DropColumn("dbo.Inteligente", "CatalogoId");
            CreateIndex("dbo.Inteligente", "SensorId");
            AddForeignKey("dbo.Inteligente", "SensorId", "dbo.Sensor", "Id");
        }
    }
}
