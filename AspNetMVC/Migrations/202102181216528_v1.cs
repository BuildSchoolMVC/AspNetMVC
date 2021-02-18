namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "AccountName", c => c.String(nullable: false));
            AddColumn("dbo.UserFavorites", "AccountName", c => c.String(nullable: false));
            DropColumn("dbo.Orders", "AccountId");
            DropColumn("dbo.UserFavorites", "AccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserFavorites", "AccountId", c => c.Guid(nullable: false));
            AddColumn("dbo.Orders", "AccountId", c => c.Guid(nullable: false));
            DropColumn("dbo.UserFavorites", "AccountName");
            DropColumn("dbo.Orders", "AccountName");
        }
    }
}
