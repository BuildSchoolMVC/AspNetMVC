namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0223 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accounts", "IsThirdParty");
            DropColumn("dbo.Accounts", "IsIntegrated");
            DropColumn("dbo.Accounts", "SocialPlatform");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "SocialPlatform", c => c.String());
            AddColumn("dbo.Accounts", "IsIntegrated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Accounts", "IsThirdParty", c => c.Boolean(nullable: false));
        }
    }
}
