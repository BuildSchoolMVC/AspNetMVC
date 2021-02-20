namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberCreditCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberMds", "Phone", c => c.String());
            AddColumn("dbo.MemberMds", "Mail", c => c.String());
            AddColumn("dbo.MemberMds", "Address", c => c.String());
            AddColumn("dbo.MemberMds", "Password", c => c.String());
            DropColumn("dbo.MemberMds", "CreditNumber");
            DropColumn("dbo.MemberMds", "ExpiryDate");
            DropColumn("dbo.MemberMds", "SafeNum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberMds", "SafeNum", c => c.Int(nullable: false));
            AddColumn("dbo.MemberMds", "ExpiryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MemberMds", "CreditNumber", c => c.Int(nullable: false));
            DropColumn("dbo.MemberMds", "Password");
            DropColumn("dbo.MemberMds", "Address");
            DropColumn("dbo.MemberMds", "Mail");
            DropColumn("dbo.MemberMds", "Phone");
        }
    }
}
