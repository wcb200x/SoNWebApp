namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeprogidasfktoPOS : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.POS", "ProgramID", "dbo.Programs");
            DropIndex("dbo.POS", new[] { "ProgramID" });
            DropColumn("dbo.POS", "ProgramID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.POS", "ProgramID", c => c.Int(nullable: false));
            CreateIndex("dbo.POS", "ProgramID");
            AddForeignKey("dbo.POS", "ProgramID", "dbo.Programs", "ID", cascadeDelete: false    );
        }
    }
}
