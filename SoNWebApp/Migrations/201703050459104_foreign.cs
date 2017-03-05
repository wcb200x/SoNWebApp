namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreign : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "ProgramID_ID", c => c.Int());
            CreateIndex("dbo.Courses", "ProgramID_ID");
            AddForeignKey("dbo.Courses", "ProgramID_ID", "dbo.Programs", "ID");
            DropColumn("dbo.Courses", "ProgramID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "ProgramID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Courses", "ProgramID_ID", "dbo.Programs");
            DropIndex("dbo.Courses", new[] { "ProgramID_ID" });
            DropColumn("dbo.Courses", "ProgramID_ID");
        }
    }
}
