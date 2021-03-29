namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Backstage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderPackages",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        RoomTypes = c.String(nullable: false),
                        SquareFeets = c.String(nullable: false),
                        ServiceItems = c.String(nullable: false),
                        Hour = c.Single(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderUserDefineds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Guid(nullable: false),
                        RoomType = c.Int(nullable: false),
                        SquareFeet = c.Int(nullable: false),
                        ServiceItems = c.String(nullable: false),
                        Hour = c.Single(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PackageProducts", "IsValid", c => c.Boolean(nullable: false));
            AddColumn("dbo.PackageProducts", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PackageProducts", "IsDelete");
            DropColumn("dbo.PackageProducts", "IsValid");
            DropTable("dbo.OrderUserDefineds");
            DropTable("dbo.OrderPackages");
        }
    }
}
