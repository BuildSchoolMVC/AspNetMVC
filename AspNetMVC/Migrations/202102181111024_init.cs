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
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        PackageProductId = c.Int(nullable: false),
                        Star = c.Int(nullable: false),
                        Content = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.CommentId);
            
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
                "dbo.MemberMds",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        Name = c.String(),
                        CreditNumber = c.Int(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        SafeNum = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        UserDefinedId = c.Guid(),
                        PackageProductId = c.Int(),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductName = c.String(nullable: false),
                        IsPakage = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.OrderDetailId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        DateService = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        OrderState = c.Byte(nullable: false),
                        Rate = c.Byte(),
                        Comment = c.String(),
                        CouponID = c.Guid(),
                        PaymentMethod = c.Byte(nullable: false),
                        InvoiceType = c.Byte(nullable: false),
                        InvoiceDonateTo = c.Byte(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.PackageProducts",
                c => new
                    {
                        PackageProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoomType = c.Int(nullable: false),
                        RoomType2 = c.Int(nullable: false),
                        RoomType3 = c.Int(),
                        ServiceItem = c.String(),
                        Squarefeet = c.Int(nullable: false),
                        Squarefeet2 = c.Int(nullable: false),
                        Squarefeet3 = c.Int(),
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
                        UserDefinedProductId = c.Guid(nullable: false),
                        UserDefinedId = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        Name = c.String(),
                        RoomType = c.Int(nullable: false),
                        ServiceItems = c.String(),
                        Squarefeet = c.Int(nullable: false),
                        Hour = c.Single(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.UserDefinedProductId);
            
            CreateTable(
                "dbo.UserFavorites",
                c => new
                    {
                        FavoriteId = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        UserDefinedId = c.Guid(),
                        PackageProductId = c.Int(),
                        IsPakage = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.FavoriteId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserFavorites");
            DropTable("dbo.UserDefinedProducts");
            DropTable("dbo.SquareFeet");
            DropTable("dbo.SingleProducts");
            DropTable("dbo.ServiceItems");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.PackageProducts");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.MemberMds");
            DropTable("dbo.CustomerServices");
            DropTable("dbo.Comments");
            DropTable("dbo.Accounts");
        }
    }
}
