namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "ProgramID_ID", "dbo.Programs");
            DropIndex("dbo.Courses", new[] { "ProgramID_ID" });
            RenameColumn(table: "dbo.Courses", name: "ProgramID_ID", newName: "ProgramID");
            AlterColumn("dbo.Courses", "ProgramID", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "ProgramID");
            AddForeignKey("dbo.Courses", "ProgramID", "dbo.Programs", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "ProgramID", "dbo.Programs");
            DropIndex("dbo.Courses", new[] { "ProgramID" });
            AlterColumn("dbo.Courses", "ProgramID", c => c.Int());
            RenameColumn(table: "dbo.Courses", name: "ProgramID", newName: "ProgramID_ID");
            CreateIndex("dbo.Courses", "ProgramID_ID");
            AddForeignKey("dbo.Courses", "ProgramID_ID", "dbo.Programs", "ID");
        }
    }
}
