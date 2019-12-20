namespace power_aoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.results", "element_number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.results", "element_number", c => c.String(unicode: false));
        }
    }
}
