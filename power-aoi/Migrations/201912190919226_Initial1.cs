namespace power_aoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.results", "is_front", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.results", "is_front", c => c.Long(nullable: false));
        }
    }
}
