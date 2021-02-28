namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "FavoriteId", c => c.Guid(nullable: false));
            DropColumn("dbo.OrderDetails", "UserDefinedId");
            DropColumn("dbo.OrderDetails", "PackageProductId");
            DropColumn("dbo.OrderDetails", "IsPackage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "IsPackage", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetails", "PackageProductId", c => c.Int());
            AddColumn("dbo.OrderDetails", "UserDefinedId", c => c.Guid());
            DropColumn("dbo.OrderDetails", "FavoriteId");
        }
    }
}
