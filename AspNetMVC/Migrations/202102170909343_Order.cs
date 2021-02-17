namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        RoomTypeId = c.Int(nullable: false),
                        ServiceItem = c.String(nullable: false),
                        SquareFeet = c.Int(nullable: false),
                        Hours = c.Double(nullable: false),
                        PackageProductId = c.Int(nullable: false),
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
                        FavoriteId = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateService = c.DateTime(nullable: false),
                        TimeService = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        OrderState = c.Byte(nullable: false),
                        Rate = c.Byte(nullable: false),
                        Comment = c.String(),
                        CouponID = c.Guid(nullable: false),
                        PaymentMethod = c.Byte(nullable: false),
                        InvoiceType = c.Byte(nullable: false),
                        InvoiceDonateTo = c.Byte(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
