namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coupon_IsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coupons", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Coupons", "IsActive");
        }
    }
}
