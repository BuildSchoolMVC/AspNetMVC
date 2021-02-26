namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "FullName", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "Remark", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Remark");
            DropColumn("dbo.Orders", "Phone");
            DropColumn("dbo.Orders", "Email");
            DropColumn("dbo.Orders", "FullName");
        }
    }
}
