namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiosCliente : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cliente", "CategoriaId", "dbo.Categoria");
            DropForeignKey("dbo.Cliente", "TransformadorId", "dbo.Transformador");
            DropIndex("dbo.Cliente", new[] { "TransformadorId" });
            DropIndex("dbo.Cliente", new[] { "CategoriaId" });
            AlterColumn("dbo.Cliente", "TransformadorId", c => c.Int());
            AlterColumn("dbo.Cliente", "CategoriaId", c => c.Int());
            CreateIndex("dbo.Cliente", "TransformadorId");
            CreateIndex("dbo.Cliente", "CategoriaId");
            AddForeignKey("dbo.Cliente", "CategoriaId", "dbo.Categoria", "Id");
            AddForeignKey("dbo.Cliente", "TransformadorId", "dbo.Transformador", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "TransformadorId", "dbo.Transformador");
            DropForeignKey("dbo.Cliente", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.Cliente", new[] { "CategoriaId" });
            DropIndex("dbo.Cliente", new[] { "TransformadorId" });
            AlterColumn("dbo.Cliente", "CategoriaId", c => c.Int(nullable: false));
            AlterColumn("dbo.Cliente", "TransformadorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cliente", "CategoriaId");
            CreateIndex("dbo.Cliente", "TransformadorId");
            AddForeignKey("dbo.Cliente", "TransformadorId", "dbo.Transformador", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cliente", "CategoriaId", "dbo.Categoria", "Id", cascadeDelete: true);
        }
    }
}
