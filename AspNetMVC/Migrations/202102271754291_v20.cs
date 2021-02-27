namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ECPayParams",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        MerchantTradeNo = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
            AlterColumn("dbo.Orders", "MerchantTradeNo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "MerchantTradeNo", c => c.String());
            DropTable("dbo.ECPayParams");
        }
    }
}
