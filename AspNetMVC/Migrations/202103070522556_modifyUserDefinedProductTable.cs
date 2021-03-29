namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyUserDefinedProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDefinedProducts", "IsDelete", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserDefinedProducts", "Index");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDefinedProducts", "Index", c => c.Int(nullable: false));
            DropColumn("dbo.UserDefinedProducts", "IsDelete");
        }
    }
}
