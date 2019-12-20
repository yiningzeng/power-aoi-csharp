namespace power_aoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.results", "is_misjudge", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.results", "is_misjudge", c => c.String(unicode: false));
        }
    }
}
