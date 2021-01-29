namespace DWA_Assignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMeetIdtoEvent : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Events", name: "Meet_MeetId", newName: "MeetId");
            RenameIndex(table: "dbo.Events", name: "IX_Meet_MeetId", newName: "IX_MeetId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Events", name: "IX_MeetId", newName: "IX_Meet_MeetId");
            RenameColumn(table: "dbo.Events", name: "MeetId", newName: "Meet_MeetId");
        }
    }
}
