namespace DWA_Assignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGroupNametofamilygroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FamilyGroups", "GroupName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FamilyGroups", "GroupName");
        }
    }
}
