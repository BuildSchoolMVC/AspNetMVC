namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerServices", "CreatedTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerServices", "CreatedTime", c => c.DateTime(nullable: false));
        }
    }
}
