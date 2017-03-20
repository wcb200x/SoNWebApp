namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeenrollment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollments", "Student_ID", "dbo.Students");
            DropIndex("dbo.Enrollments", new[] { "Student_ID" });
            RenameColumn(table: "dbo.Enrollments", name: "Student_ID", newName: "StudentID");
            AlterColumn("dbo.Enrollments", "StudentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Enrollments", "StudentID");
            AddForeignKey("dbo.Enrollments", "StudentID", "dbo.Students", "ID", cascadeDelete: false);
            DropColumn("dbo.Enrollments", "StudentNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enrollments", "StudentNumber", c => c.Int(nullable: false));
            DropForeignKey("dbo.Enrollments", "StudentID", "dbo.Students");
            DropIndex("dbo.Enrollments", new[] { "StudentID" });
            AlterColumn("dbo.Enrollments", "StudentID", c => c.Int());
            RenameColumn(table: "dbo.Enrollments", name: "StudentID", newName: "Student_ID");
            CreateIndex("dbo.Enrollments", "Student_ID");
            AddForeignKey("dbo.Enrollments", "Student_ID", "dbo.Students", "ID");
        }
    }
}
