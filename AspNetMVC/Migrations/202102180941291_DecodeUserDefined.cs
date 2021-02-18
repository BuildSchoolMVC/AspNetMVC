namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DecodeUserDefined : DbMigration
    {
        public override void Up()
        {
            CreateTable(
              "dbo.UserDefinedProducts",
              c => new
              {
                  UserDefinedProductId=c.Int(nullable: false),
                  UserDefinedId = c.Guid(nullable: false),
                  MemberId = c.Guid(nullable: false),
                  Name = c.String(),
                  RoomType = c.Int(),
                  ServiceItems = c.String(),
                  Squarefeet = c.Int(),
                  Hour = c.Single(nullable: false),
                  Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                  CreateTime = c.DateTime(nullable: false),
                  CreateUser = c.String(),
                  EditTime = c.DateTime(nullable: false),
                  EditUser = c.String(),
              })
              .PrimaryKey(t => t.UserDefinedProductId);
        }
        
        public override void Down()
        {
            DropTable("dbo.UserDefinedProducts");
        }
    }
}
