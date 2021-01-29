namespace DWA_Assignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMeetsEventsFamilyGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        AgeRange = c.Int(nullable: false),
                        Gender = c.String(),
                        Distance = c.String(),
                        Stroke = c.String(),
                        Round = c.Int(nullable: false),
                        Meet_MeetId = c.Int(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Meets", t => t.Meet_MeetId)
                .Index(t => t.Meet_MeetId);
            
            CreateTable(
                "dbo.FamilyGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Parent_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.AspNetUsers", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Meets",
                c => new
                    {
                        MeetId = c.Int(nullable: false, identity: true),
                        MeetName = c.String(maxLength: 20),
                        Date = c.DateTime(nullable: false),
                        MyProperty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MeetId);
            
            AddColumn("dbo.AspNetUsers", "Event_EventId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "FamilyGroup_GroupId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Event_EventId");
            CreateIndex("dbo.AspNetUsers", "FamilyGroup_GroupId");
            AddForeignKey("dbo.AspNetUsers", "Event_EventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.AspNetUsers", "FamilyGroup_GroupId", "dbo.FamilyGroups", "GroupId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Meet_MeetId", "dbo.Meets");
            DropForeignKey("dbo.AspNetUsers", "FamilyGroup_GroupId", "dbo.FamilyGroups");
            DropForeignKey("dbo.FamilyGroups", "Parent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Event_EventId", "dbo.Events");
            DropIndex("dbo.FamilyGroups", new[] { "Parent_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "FamilyGroup_GroupId" });
            DropIndex("dbo.AspNetUsers", new[] { "Event_EventId" });
            DropIndex("dbo.Events", new[] { "Meet_MeetId" });
            DropColumn("dbo.AspNetUsers", "FamilyGroup_GroupId");
            DropColumn("dbo.AspNetUsers", "Event_EventId");
            DropTable("dbo.Meets");
            DropTable("dbo.FamilyGroups");
            DropTable("dbo.Events");
        }
    }
}
