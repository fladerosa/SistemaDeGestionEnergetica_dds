namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambio3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SensorFisico", "Id", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
        }
    }
}
