namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberCreditCard : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MemberCreditCards", "SafeNum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberCreditCards", "SafeNum", c => c.Int(nullable: false));
        }
    }
}
