namespace DWA_Assignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedEventtoEventIdinLane : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Lanes", name: "eve_EventId", newName: "EventId");
            RenameIndex(table: "dbo.Lanes", name: "IX_eve_EventId", newName: "IX_EventId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Lanes", name: "IX_EventId", newName: "IX_eve_EventId");
            RenameColumn(table: "dbo.Lanes", name: "EventId", newName: "eve_EventId");
        }
    }
}
