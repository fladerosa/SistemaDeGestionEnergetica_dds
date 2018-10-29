namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enumTipoOperacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Condicion", "tipoOperacion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Condicion", "tipoOperacion");
        }
    }
}
