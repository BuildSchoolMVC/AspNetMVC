namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifycolumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "AccountName", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "AccountName", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
