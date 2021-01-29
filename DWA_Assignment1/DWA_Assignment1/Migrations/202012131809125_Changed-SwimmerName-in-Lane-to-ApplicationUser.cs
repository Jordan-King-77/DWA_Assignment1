namespace DWA_Assignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedSwimmerNameinLanetoApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lanes", "Swimmer_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Lanes", "Swimmer_Id");
            AddForeignKey("dbo.Lanes", "Swimmer_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Lanes", "SwimmerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lanes", "SwimmerName", c => c.String());
            DropForeignKey("dbo.Lanes", "Swimmer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Lanes", new[] { "Swimmer_Id" });
            DropColumn("dbo.Lanes", "Swimmer_Id");
        }
    }
}
