namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionCatalogo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catalogo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        ConsumoEnergia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdentificadorFabrica = c.String(),
                        AdministradorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Administrador", t => t.AdministradorId)
                .Index(t => t.AdministradorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Catalogo", "AdministradorId", "dbo.Administrador");
            DropIndex("dbo.Catalogo", new[] { "AdministradorId" });
            DropTable("dbo.Catalogo");
        }
    }
}
