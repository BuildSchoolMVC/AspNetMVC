namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreditCard : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MemberCreditCards", "CreditNumber", c => c.String(nullable: false, maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MemberCreditCards", "CreditNumber", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
