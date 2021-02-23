namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddThirdPartyAccountColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "IsThirdParty", c => c.Boolean(nullable: false));
            AddColumn("dbo.Accounts", "IsIntegrated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Accounts", "SocialPlatform", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "SocialPlatform");
            DropColumn("dbo.Accounts", "IsIntegrated");
            DropColumn("dbo.Accounts", "IsThirdParty");
        }
    }
}
