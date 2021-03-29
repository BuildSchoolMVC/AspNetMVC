namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerService_addColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerServices", "Reply", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerServices", "Reply");
        }
    }
}
