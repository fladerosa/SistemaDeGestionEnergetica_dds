namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class largoNombreDispositivo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dispositivo", "Nombre", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dispositivo", "Nombre", c => c.String(nullable: false, maxLength: 25));
        }
    }
}
