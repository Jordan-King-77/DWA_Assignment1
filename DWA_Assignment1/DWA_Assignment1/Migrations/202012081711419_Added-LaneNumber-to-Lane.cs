namespace DWA_Assignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLaneNumbertoLane : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lanes", "LaneNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lanes", "LaneNumber");
        }
    }
}
