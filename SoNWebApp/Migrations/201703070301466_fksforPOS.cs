namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fksforPOS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POS", "ProgramID", c => c.Int(nullable: false));
            AddColumn("dbo.POS", "StudentID", c => c.Int(nullable: false));
            CreateIndex("dbo.POS", "ProgramID");
            CreateIndex("dbo.POS", "StudentID");
            AddForeignKey("dbo.POS", "ProgramID", "dbo.Programs", "ID", cascadeDelete: false);
            AddForeignKey("dbo.POS", "StudentID", "dbo.Students", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.POS", "StudentID", "dbo.Students");
            DropForeignKey("dbo.POS", "ProgramID", "dbo.Programs");
            DropIndex("dbo.POS", new[] { "StudentID" });
            DropIndex("dbo.POS", new[] { "ProgramID" });
            DropColumn("dbo.POS", "StudentID");
            DropColumn("dbo.POS", "ProgramID");
        }
    }
}
