namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Favorite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        FavoriteId = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        UserDefinedId = c.Guid(nullable: false),
                        PackageProductId = c.Int(nullable: false),
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
            DropTable("dbo.Favorites");
        }
    }
}
