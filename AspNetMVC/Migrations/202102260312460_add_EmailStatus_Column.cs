namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_EmailStatus_Column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "EmailStatus", c => c.String());
            DropColumn("dbo.Accounts", "EmailVerification");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "EmailVerification", c => c.Boolean(nullable: false));
            DropColumn("dbo.Accounts", "EmailStatus");
        }
    }
}
