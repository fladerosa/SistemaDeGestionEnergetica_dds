namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idOperador : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Condicion", "Operador_Id", "dbo.Operadores");
            DropIndex("dbo.Condicion", new[] { "Operador_Id" });
            RenameColumn(table: "dbo.Condicion", name: "Operador_Id", newName: "OperadorId");
            AlterColumn("dbo.Condicion", "OperadorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Condicion", "OperadorId");
            AddForeignKey("dbo.Condicion", "OperadorId", "dbo.Operadores", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Condicion", "OperadorId", "dbo.Operadores");
            DropIndex("dbo.Condicion", new[] { "OperadorId" });
            AlterColumn("dbo.Condicion", "OperadorId", c => c.Int());
            RenameColumn(table: "dbo.Condicion", name: "OperadorId", newName: "Operador_Id");
            CreateIndex("dbo.Condicion", "Operador_Id");
            AddForeignKey("dbo.Condicion", "Operador_Id", "dbo.Operadores", "Id");
        }
    }
}
