namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberCreditCard : DbMigration
    {
        public override void Up()
        {
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
        
        public override void Down()
        {
            DropTable("dbo.MemberCreditCards");
        }
    }
}
