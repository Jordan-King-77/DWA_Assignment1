namespace DWA_Assignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVenueandPoolLengthtoMeet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meets", "Venue", c => c.String());
            AddColumn("dbo.Meets", "PoolLength", c => c.String());
            DropColumn("dbo.Meets", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meets", "MyProperty", c => c.Int(nullable: false));
            DropColumn("dbo.Meets", "PoolLength");
            DropColumn("dbo.Meets", "Venue");
        }
    }
}
