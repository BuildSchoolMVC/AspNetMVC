namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_membermd : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.MemberCreditCards");
            DropTable("dbo.MemberMds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MemberMds",
                c => new
                    {
                        AccountId = c.Guid(nullable: false),
                        Name = c.String(),
                        Phone = c.String(),
                        Mail = c.String(),
                        Address = c.String(),
                        Password = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.MemberCreditCards",
                c => new
                    {
                        CreditNumber = c.String(nullable: false, maxLength: 16),
                        AccountName = c.String(),
                        ExpiryDate = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        EditTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.CreditNumber);
            
        }
    }
}
