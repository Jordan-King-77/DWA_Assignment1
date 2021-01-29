namespace DWA_Assignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLanelisttoEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Lane1_LaneId", "dbo.Lanes");
            DropForeignKey("dbo.Events", "Lane2_LaneId", "dbo.Lanes");
            DropForeignKey("dbo.Events", "Lane3_LaneId", "dbo.Lanes");
            DropForeignKey("dbo.Events", "Lane4_LaneId", "dbo.Lanes");
            DropForeignKey("dbo.Events", "Lane5_LaneId", "dbo.Lanes");
            DropIndex("dbo.Events", new[] { "Lane1_LaneId" });
            DropIndex("dbo.Events", new[] { "Lane2_LaneId" });
            DropIndex("dbo.Events", new[] { "Lane3_LaneId" });
            DropIndex("dbo.Events", new[] { "Lane4_LaneId" });
            DropIndex("dbo.Events", new[] { "Lane5_LaneId" });
            AddColumn("dbo.Lanes", "Event_EventId", c => c.Int());
            CreateIndex("dbo.Lanes", "Event_EventId");
            AddForeignKey("dbo.Lanes", "Event_EventId", "dbo.Events", "EventId");
            DropColumn("dbo.Events", "Lane1_LaneId");
            DropColumn("dbo.Events", "Lane2_LaneId");
            DropColumn("dbo.Events", "Lane3_LaneId");
            DropColumn("dbo.Events", "Lane4_LaneId");
            DropColumn("dbo.Events", "Lane5_LaneId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Lane5_LaneId", c => c.Int());
            AddColumn("dbo.Events", "Lane4_LaneId", c => c.Int());
            AddColumn("dbo.Events", "Lane3_LaneId", c => c.Int());
            AddColumn("dbo.Events", "Lane2_LaneId", c => c.Int());
            AddColumn("dbo.Events", "Lane1_LaneId", c => c.Int());
            DropForeignKey("dbo.Lanes", "Event_EventId", "dbo.Events");
            DropIndex("dbo.Lanes", new[] { "Event_EventId" });
            DropColumn("dbo.Lanes", "Event_EventId");
            CreateIndex("dbo.Events", "Lane5_LaneId");
            CreateIndex("dbo.Events", "Lane4_LaneId");
            CreateIndex("dbo.Events", "Lane3_LaneId");
            CreateIndex("dbo.Events", "Lane2_LaneId");
            CreateIndex("dbo.Events", "Lane1_LaneId");
            AddForeignKey("dbo.Events", "Lane5_LaneId", "dbo.Lanes", "LaneId");
            AddForeignKey("dbo.Events", "Lane4_LaneId", "dbo.Lanes", "LaneId");
            AddForeignKey("dbo.Events", "Lane3_LaneId", "dbo.Lanes", "LaneId");
            AddForeignKey("dbo.Events", "Lane2_LaneId", "dbo.Lanes", "LaneId");
            AddForeignKey("dbo.Events", "Lane1_LaneId", "dbo.Lanes", "LaneId");
        }
    }
}
