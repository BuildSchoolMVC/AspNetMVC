namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserdifineAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDefinedProducts", "ServiceItems", c => c.String());
            AlterColumn("dbo.UserDefinedProducts", "MemberId", c => c.Guid(nullable: false));
            AlterColumn("dbo.UserDefinedProducts", "RoomType", c => c.Int(nullable: false));
            AlterColumn("dbo.UserDefinedProducts", "Squarefeet", c => c.Int(nullable: false));
            DropColumn("dbo.UserDefinedProducts", "ServiceItem");
            DropColumn("dbo.UserDefinedProducts", "PhotoUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDefinedProducts", "PhotoUrl", c => c.String());
            AddColumn("dbo.UserDefinedProducts", "ServiceItem", c => c.String());
            AlterColumn("dbo.UserDefinedProducts", "Squarefeet", c => c.String());
            AlterColumn("dbo.UserDefinedProducts", "RoomType", c => c.String());
            AlterColumn("dbo.UserDefinedProducts", "MemberId", c => c.Int(nullable: false));
            DropColumn("dbo.UserDefinedProducts", "ServiceItems");
        }
    }
}
