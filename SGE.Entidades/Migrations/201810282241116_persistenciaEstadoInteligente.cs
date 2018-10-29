namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class persistenciaEstadoInteligente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inteligente", "Estado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inteligente", "Estado");
        }
    }
}
