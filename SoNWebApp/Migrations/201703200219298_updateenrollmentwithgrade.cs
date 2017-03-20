namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateenrollmentwithgrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enrollments", "Grade", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enrollments", "Grade");
        }
    }
}
