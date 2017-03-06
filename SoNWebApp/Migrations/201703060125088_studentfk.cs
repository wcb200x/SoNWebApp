namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentfk : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Students", "CampusID");
            CreateIndex("dbo.Students", "ProgramID");
            AddForeignKey("dbo.Students", "CampusID", "dbo.Campus", "CampusID", cascadeDelete: false);
            AddForeignKey("dbo.Students", "ProgramID", "dbo.Programs", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ProgramID", "dbo.Programs");
            DropForeignKey("dbo.Students", "CampusID", "dbo.Campus");
            DropIndex("dbo.Students", new[] { "ProgramID" });
            DropIndex("dbo.Students", new[] { "CampusID" });
        }
    }
}
