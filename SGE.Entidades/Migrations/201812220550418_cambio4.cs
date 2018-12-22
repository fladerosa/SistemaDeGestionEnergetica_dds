namespace SGE.Entidades.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambio4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SensorFisico", "Descripcion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SensorFisico", "Descripcion");
        }
    }
}
