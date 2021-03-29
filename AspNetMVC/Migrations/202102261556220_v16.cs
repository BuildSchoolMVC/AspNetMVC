namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v16 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "InvoiceDonateTo", c => c.Byte());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "InvoiceDonateTo", c => c.Byte(nullable: false));
        }
    }
}
