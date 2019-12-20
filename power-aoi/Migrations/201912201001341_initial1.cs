namespace power_aoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.results", "element_number", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.results", "element_number");
        }
    }
}
