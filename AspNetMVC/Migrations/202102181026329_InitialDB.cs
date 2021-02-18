namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserDefinedProducts");
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
            
            AddPrimaryKey("dbo.UserDefinedProducts", "UserDefinedId");
            DropColumn("dbo.UserDefinedProducts", "UserDefinedProductId");
            DropTable("dbo.UserFavorites");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.UserDefinedProducts", "UserDefinedProductId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.UserDefinedProducts");
            DropTable("dbo.MemberMds");
            AddPrimaryKey("dbo.UserDefinedProducts", "UserDefinedProductId");
        }
    }
}
