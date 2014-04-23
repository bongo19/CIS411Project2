namespace MeetUs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Spots",
                c => new
                    {
                        SpotId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SpotId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Spots");
        }
    }
}
