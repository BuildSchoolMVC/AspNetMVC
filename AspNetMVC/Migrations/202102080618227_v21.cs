namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "EditTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CustomerServices", "EditTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerServices", "EditTime", c => c.DateTime());
            AlterColumn("dbo.Accounts", "EditTime", c => c.DateTime());
        }
    }
}
