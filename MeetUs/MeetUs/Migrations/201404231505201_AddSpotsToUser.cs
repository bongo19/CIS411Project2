namespace MeetUs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpotsToUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfile", "Spot_SpotId", "dbo.Spots");
            DropIndex("dbo.UserProfile", new[] { "Spot_SpotId" });
            CreateTable(
                "dbo.SpotUserProfiles",
                c => new
                    {
                        Spot_SpotId = c.Int(nullable: false),
                        UserProfile_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Spot_SpotId, t.UserProfile_UserId })
                .ForeignKey("dbo.Spots", t => t.Spot_SpotId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_UserId, cascadeDelete: true)
                .Index(t => t.Spot_SpotId)
                .Index(t => t.UserProfile_UserId);
            
            DropColumn("dbo.UserProfile", "Spot_SpotId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfile", "Spot_SpotId", c => c.Int());
            DropIndex("dbo.SpotUserProfiles", new[] { "UserProfile_UserId" });
            DropIndex("dbo.SpotUserProfiles", new[] { "Spot_SpotId" });
            DropForeignKey("dbo.SpotUserProfiles", "UserProfile_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.SpotUserProfiles", "Spot_SpotId", "dbo.Spots");
            DropTable("dbo.SpotUserProfiles");
            CreateIndex("dbo.UserProfile", "Spot_SpotId");
            AddForeignKey("dbo.UserProfile", "Spot_SpotId", "dbo.Spots", "SpotId");
        }
    }
}
