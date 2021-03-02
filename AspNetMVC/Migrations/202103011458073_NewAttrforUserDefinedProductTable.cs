namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewAttrforUserDefinedProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDefinedProducts", "Index", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserDefinedProducts", "Index");
        }
    }
}
