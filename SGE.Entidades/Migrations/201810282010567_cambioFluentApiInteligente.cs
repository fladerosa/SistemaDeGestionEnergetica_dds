namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambioFluentApiInteligente : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Inteligente_X_Cliente", newName: "ClienteInteligentes");
            RenameColumn(table: "dbo.ClienteInteligentes", name: "InteligenteId", newName: "Inteligente_Id");
            RenameColumn(table: "dbo.ClienteInteligentes", name: "ClienteId", newName: "Cliente_Id");
            RenameIndex(table: "dbo.ClienteInteligentes", name: "IX_ClienteId", newName: "IX_Cliente_Id");
            RenameIndex(table: "dbo.ClienteInteligentes", name: "IX_InteligenteId", newName: "IX_Inteligente_Id");
            DropPrimaryKey("dbo.ClienteInteligentes");
            AddPrimaryKey("dbo.ClienteInteligentes", new[] { "Cliente_Id", "Inteligente_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ClienteInteligentes");
            AddPrimaryKey("dbo.ClienteInteligentes", new[] { "InteligenteId", "ClienteId" });
            RenameIndex(table: "dbo.ClienteInteligentes", name: "IX_Inteligente_Id", newName: "IX_InteligenteId");
            RenameIndex(table: "dbo.ClienteInteligentes", name: "IX_Cliente_Id", newName: "IX_ClienteId");
            RenameColumn(table: "dbo.ClienteInteligentes", name: "Cliente_Id", newName: "ClienteId");
            RenameColumn(table: "dbo.ClienteInteligentes", name: "Inteligente_Id", newName: "InteligenteId");
            RenameTable(name: "dbo.ClienteInteligentes", newName: "Inteligente_X_Cliente");
        }
    }
}
