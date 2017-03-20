namespace SoNWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changestodisplaynames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UDApplications", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "MiddleName", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "StreetAddress", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "StreetAddress2", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "City", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "State", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "HomeNumber", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "CellNumber", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "CurrentCourses", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "PersonalQualEssay", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "NurseExperience", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "Legal1", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "Legal2", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "Legal3", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "Legal4", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "Legal5", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "Legal6", c => c.String(nullable: false));
            AlterColumn("dbo.UDApplications", "ConfirmLegal", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UDApplications", "ConfirmLegal", c => c.String());
            AlterColumn("dbo.UDApplications", "Legal6", c => c.String());
            AlterColumn("dbo.UDApplications", "Legal5", c => c.String());
            AlterColumn("dbo.UDApplications", "Legal4", c => c.String());
            AlterColumn("dbo.UDApplications", "Legal3", c => c.String());
            AlterColumn("dbo.UDApplications", "Legal2", c => c.String());
            AlterColumn("dbo.UDApplications", "Legal1", c => c.String());
            AlterColumn("dbo.UDApplications", "NurseExperience", c => c.String());
            AlterColumn("dbo.UDApplications", "PersonalQualEssay", c => c.String());
            AlterColumn("dbo.UDApplications", "CurrentCourses", c => c.String());
            AlterColumn("dbo.UDApplications", "CellNumber", c => c.String());
            AlterColumn("dbo.UDApplications", "HomeNumber", c => c.String());
            AlterColumn("dbo.UDApplications", "State", c => c.String());
            AlterColumn("dbo.UDApplications", "City", c => c.String());
            AlterColumn("dbo.UDApplications", "StreetAddress2", c => c.String());
            AlterColumn("dbo.UDApplications", "StreetAddress", c => c.String());
            AlterColumn("dbo.UDApplications", "Email", c => c.String());
            AlterColumn("dbo.UDApplications", "LastName", c => c.String());
            AlterColumn("dbo.UDApplications", "MiddleName", c => c.String());
            AlterColumn("dbo.UDApplications", "FirstName", c => c.String());
        }
    }
}
