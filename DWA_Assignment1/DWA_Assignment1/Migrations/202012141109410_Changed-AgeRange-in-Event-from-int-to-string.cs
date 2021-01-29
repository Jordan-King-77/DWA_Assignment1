namespace DWA_Assignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAgeRangeinEventfrominttostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "AgeRange", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "AgeRange", c => c.Int(nullable: false));
        }
    }
}
