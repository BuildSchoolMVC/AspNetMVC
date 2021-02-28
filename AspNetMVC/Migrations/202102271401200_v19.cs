namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v19 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PaymentType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PaymentType", c => c.String(nullable: false));
        }
    }
}
