namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDefinedProducts", "AccountName", c => c.String());
            DropColumn("dbo.UserDefinedProducts", "MemberId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDefinedProducts", "MemberId", c => c.Guid(nullable: false));
            DropColumn("dbo.UserDefinedProducts", "AccountName");
        }
    }
}
