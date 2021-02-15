namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v31 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PackageProducts",
                c => new
                    {
                        PackageProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoomType = c.String(),
                        ServiceItem = c.String(),
                        Squarefeet = c.String(),
                        Hour = c.Single(nullable: false),
                        Description = c.String(),
                        PackagePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Url = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.PackageProductId);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        RoomTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.RoomTypeId);
            
            CreateTable(
                "dbo.ServiceItems",
                c => new
                    {
                        ServiceitemId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.ServiceitemId);
            
            CreateTable(
                "dbo.SingleProducts",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoomType = c.String(),
                        ServiceItem = c.String(),
                        Squarefeet = c.String(),
                        Hour = c.Single(nullable: false),
                        Desciption = c.String(),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Url = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.SquareFeet",
                c => new
                    {
                        SquareFeetId = c.Int(nullable: false, identity: true),
                        SquareFeetValue = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.SquareFeetId);
            
            CreateTable(
                "dbo.UserDefinedProducts",
                c => new
                    {
                        UserDefinedGUID = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        Name = c.String(),
                        RoomType = c.String(),
                        ServiceItem = c.String(),
                        Squarefeet = c.String(),
                        Hour = c.Single(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Url = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.UserDefinedGUID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserDefinedProducts");
            DropTable("dbo.SquareFeet");
            DropTable("dbo.SingleProducts");
            DropTable("dbo.ServiceItems");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.PackageProducts");
        }
    }
}
