namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "IsPackage", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserFavorites", "IsPackage", c => c.Boolean(nullable: false));
            DropColumn("dbo.OrderDetails", "IsPakage");
            DropColumn("dbo.UserFavorites", "IsPakage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserFavorites", "IsPakage", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetails", "IsPakage", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserFavorites", "IsPackage");
            DropColumn("dbo.OrderDetails", "IsPackage");
        }
    }
}
