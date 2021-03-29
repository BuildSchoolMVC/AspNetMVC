namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteSingleProductTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.SingleProducts");
        }
        
        public override void Down()
        {
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
            
        }
    }
}
