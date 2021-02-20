namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberCreditCard1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberCreditCards",
                c => new
                    {
                        AccountName = c.String(nullable: false, maxLength: 128),
                        CreditNumber = c.Int(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        SafeNum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountName);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MemberCreditCards");
        }
    }
}
