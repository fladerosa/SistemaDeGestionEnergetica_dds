namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clienteMapa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cliente", "TransformadorId", "dbo.Transformador");
            DropIndex("dbo.Cliente", new[] { "TransformadorId" });
            AlterColumn("dbo.Cliente", "TransformadorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cliente", "TransformadorId");
            AddForeignKey("dbo.Cliente", "TransformadorId", "dbo.Transformador", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "TransformadorId", "dbo.Transformador");
            DropIndex("dbo.Cliente", new[] { "TransformadorId" });
            AlterColumn("dbo.Cliente", "TransformadorId", c => c.Int());
            CreateIndex("dbo.Cliente", "TransformadorId");
            AddForeignKey("dbo.Cliente", "TransformadorId", "dbo.Transformador", "Id");
        }
    }
}
