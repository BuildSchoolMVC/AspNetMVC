namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v17 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Remark", c => c.String(nullable: false));
        }
    }
}
