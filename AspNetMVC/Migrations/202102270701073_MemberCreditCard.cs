namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberCreditCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberCreditCards", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MemberCreditCards", "CreateUser", c => c.String());
            AddColumn("dbo.MemberCreditCards", "EditTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MemberCreditCards", "EditUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberCreditCards", "EditUser");
            DropColumn("dbo.MemberCreditCards", "EditTime");
            DropColumn("dbo.MemberCreditCards", "CreateUser");
            DropColumn("dbo.MemberCreditCards", "CreateTime");
        }
    }
}
