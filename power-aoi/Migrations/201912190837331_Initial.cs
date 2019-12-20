namespace power_aoi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.pcbs",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        pcb_number = c.String(unicode: false),
                        pcb_other_number = c.String(unicode: false),
                        pcb_path = c.String(unicode: false),
                        create_time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.results",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        is_front = c.Long(nullable: false),
                        pdb_id = c.Long(nullable: false),
                        element_number = c.String(unicode: false),
                        region = c.String(unicode: false),
                        ng_type = c.String(unicode: false),
                        is_misjudge = c.String(unicode: false),
                        result_string = c.String(unicode: false),
                        part_image_path = c.String(unicode: false),
                        create_time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(unicode: false),
                        password = c.String(unicode: false),
                        type = c.String(unicode: false),
                        create_time = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.users");
            DropTable("dbo.results");
            DropTable("dbo.pcbs");
        }
    }
}
