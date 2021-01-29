namespace DWA_Assignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLaneModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Event_EventId", "dbo.Events");
            DropIndex("dbo.AspNetUsers", new[] { "Event_EventId" });
            CreateTable(
                "dbo.Lanes",
                c => new
                    {
                        LaneId = c.Int(nullable: false, identity: true),
                        SwimmerName = c.String(),
                        SwimmerTime = c.DateTime(nullable: false),
                        Heat = c.String(),
                    })
                .PrimaryKey(t => t.LaneId);
            
            AddColumn("dbo.Events", "Lane1_LaneId", c => c.Int());
            AddColumn("dbo.Events", "Lane2_LaneId", c => c.Int());
            AddColumn("dbo.Events", "Lane3_LaneId", c => c.Int());
            AddColumn("dbo.Events", "Lane4_LaneId", c => c.Int());
            AddColumn("dbo.Events", "Lane5_LaneId", c => c.Int());
            CreateIndex("dbo.Events", "Lane1_LaneId");
            CreateIndex("dbo.Events", "Lane2_LaneId");
            CreateIndex("dbo.Events", "Lane3_LaneId");
            CreateIndex("dbo.Events", "Lane4_LaneId");
            CreateIndex("dbo.Events", "Lane5_LaneId");
            AddForeignKey("dbo.Events", "Lane1_LaneId", "dbo.Lanes", "LaneId");
            AddForeignKey("dbo.Events", "Lane2_LaneId", "dbo.Lanes", "LaneId");
            AddForeignKey("dbo.Events", "Lane3_LaneId", "dbo.Lanes", "LaneId");
            AddForeignKey("dbo.Events", "Lane4_LaneId", "dbo.Lanes", "LaneId");
            AddForeignKey("dbo.Events", "Lane5_LaneId", "dbo.Lanes", "LaneId");
            DropColumn("dbo.AspNetUsers", "Event_EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Event_EventId", c => c.Int());
            DropForeignKey("dbo.Events", "Lane5_LaneId", "dbo.Lanes");
            DropForeignKey("dbo.Events", "Lane4_LaneId", "dbo.Lanes");
            DropForeignKey("dbo.Events", "Lane3_LaneId", "dbo.Lanes");
            DropForeignKey("dbo.Events", "Lane2_LaneId", "dbo.Lanes");
            DropForeignKey("dbo.Events", "Lane1_LaneId", "dbo.Lanes");
            DropIndex("dbo.Events", new[] { "Lane5_LaneId" });
            DropIndex("dbo.Events", new[] { "Lane4_LaneId" });
            DropIndex("dbo.Events", new[] { "Lane3_LaneId" });
            DropIndex("dbo.Events", new[] { "Lane2_LaneId" });
            DropIndex("dbo.Events", new[] { "Lane1_LaneId" });
            DropColumn("dbo.Events", "Lane5_LaneId");
            DropColumn("dbo.Events", "Lane4_LaneId");
            DropColumn("dbo.Events", "Lane3_LaneId");
            DropColumn("dbo.Events", "Lane2_LaneId");
            DropColumn("dbo.Events", "Lane1_LaneId");
            DropTable("dbo.Lanes");
            CreateIndex("dbo.AspNetUsers", "Event_EventId");
            AddForeignKey("dbo.AspNetUsers", "Event_EventId", "dbo.Events", "EventId");
        }
    }
}
