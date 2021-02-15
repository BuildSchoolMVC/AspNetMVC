namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
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
                        EditTime = c.DateTime(),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Accounts");
        }
    }
}
