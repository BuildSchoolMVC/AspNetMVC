namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCouponandCouponDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CouponDetails",
                c => new
                    {
                        CouponDetailID = c.Guid(nullable: false),
                        CouponID = c.Int(nullable: false),
                        AccountName = c.String(),
                        State = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.CouponDetailID);
            
            CreateTable(
                "dbo.Coupons",
                c => new
                    {
                        CouponID = c.Guid(nullable: false),
                        CouponName = c.String(),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.CouponID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Coupons");
            DropTable("dbo.CouponDetails");
        }
    }
}
