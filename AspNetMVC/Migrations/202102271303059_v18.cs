namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "FinalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "CouponDetailId", c => c.Guid());
            AddColumn("dbo.Orders", "PaymentType", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "MerchantTradeNo", c => c.String());
            AddColumn("dbo.Orders", "TradeNo", c => c.String());
            DropColumn("dbo.OrderDetails", "ProductPrice");
            DropColumn("dbo.Orders", "CouponId");
            DropColumn("dbo.Orders", "PaymentMethod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PaymentMethod", c => c.Byte(nullable: false));
            AddColumn("dbo.Orders", "CouponId", c => c.Guid());
            AddColumn("dbo.OrderDetails", "ProductPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Orders", "TradeNo");
            DropColumn("dbo.Orders", "MerchantTradeNo");
            DropColumn("dbo.Orders", "PaymentType");
            DropColumn("dbo.Orders", "CouponDetailId");
            DropColumn("dbo.OrderDetails", "FinalPrice");
        }
    }
}
