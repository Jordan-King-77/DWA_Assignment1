namespace DWA_Assignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedLaneSwimmerTimefromDateTimetoTimeSpan : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lanes", "SwimmerTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lanes", "SwimmerTime", c => c.DateTime(nullable: false));
        }
    }
}
