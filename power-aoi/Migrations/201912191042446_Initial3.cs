namespace power_aoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.results", "pdb_id");
            AddForeignKey("dbo.results", "pdb_id", "dbo.pcbs", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.results", "pdb_id", "dbo.pcbs");
            DropIndex("dbo.results", new[] { "pdb_id" });
        }
    }
}
