namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        AccountName = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false),
                        Gender = c.Int(),
                        Email = c.String(nullable: false, maxLength: 50),
                        EmailVerification = c.Boolean(nullable: false),
                        Phone = c.String(maxLength: 30),
                        Address = c.String(maxLength: 100),
                        Authority = c.Int(nullable: false),
                        Remark = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.CustomerServices",
                c => new
                    {
                        CustomerServiceId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 30),
                        Email = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 20),
                        Category = c.Int(nullable: false),
                        Content = c.String(maxLength: 500),
                        IsRead = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.CustomerServiceId);
            
            CreateTable(
                "dbo.PackageProducts",
                c => new
                    {
                        PackageProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoomType = c.Int(nullable: false),
                        RoomType2 = c.Int(nullable: false),
                        RoomType3 = c.Int(nullable: false),
                        ServiceItem = c.String(),
                        Squarefeet = c.Int(nullable: false),
                        Squarefeet2 = c.Int(nullable: false),
                        Squarefeet3 = c.Int(nullable: false),
                        Hour = c.Single(nullable: false),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PhotoUrl = c.String(),
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
                        Value = c.Int(nullable: false),
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
                        Value = c.Int(nullable: false),
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
                        RoomType = c.Int(nullable: false),
                        ServiceItem = c.String(),
                        Squarefeet = c.Int(nullable: false),
                        Hour = c.Single(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PhotoUrl = c.String(),
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
                        Name = c.String(),
                        Value = c.Int(nullable: false),
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
                        UserDefinedId = c.Guid(nullable: false),
                        MemberId = c.Int(nullable: false),
                        Name = c.String(),
                        RoomType = c.String(),
                        ServiceItem = c.String(),
                        Squarefeet = c.String(),
                        Hour = c.Single(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PhotoUrl = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.UserDefinedId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserDefinedProducts");
            DropTable("dbo.SquareFeet");
            DropTable("dbo.SingleProducts");
            DropTable("dbo.ServiceItems");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.PackageProducts");
            DropTable("dbo.CustomerServices");
            DropTable("dbo.Accounts");
        }
    }
}
