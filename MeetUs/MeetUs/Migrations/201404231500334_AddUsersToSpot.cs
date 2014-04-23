namespace MeetUs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersToSpot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "Spot_SpotId", c => c.Int());
            AddForeignKey("dbo.UserProfile", "Spot_SpotId", "dbo.Spots", "SpotId");
            CreateIndex("dbo.UserProfile", "Spot_SpotId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserProfile", new[] { "Spot_SpotId" });
            DropForeignKey("dbo.UserProfile", "Spot_SpotId", "dbo.Spots");
            DropColumn("dbo.UserProfile", "Spot_SpotId");
        }
    }
}
