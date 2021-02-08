namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerserviceinit : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CustomerServices");
            AddColumn("dbo.CustomerServices", "CustomerServiceId", c => c.Guid(nullable: false));
            AddColumn("dbo.CustomerServices", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.CustomerServices", "CreateUser", c => c.String());
            AddColumn("dbo.CustomerServices", "EditTime", c => c.DateTime());
            AddColumn("dbo.CustomerServices", "EditUser", c => c.String());
            AlterColumn("dbo.CustomerServices", "Name", c => c.String(maxLength: 30));
            AlterColumn("dbo.CustomerServices", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.CustomerServices", "Phone", c => c.String(maxLength: 20));
            AlterColumn("dbo.CustomerServices", "Content", c => c.String(maxLength: 100));
            AddPrimaryKey("dbo.CustomerServices", "CustomerServiceId");
            DropColumn("dbo.CustomerServices", "Id");
            DropColumn("dbo.CustomerServices", "CreatedTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerServices", "CreatedTime", c => c.DateTime());
            AddColumn("dbo.CustomerServices", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.CustomerServices");
            AlterColumn("dbo.CustomerServices", "Content", c => c.String());
            AlterColumn("dbo.CustomerServices", "Phone", c => c.String());
            AlterColumn("dbo.CustomerServices", "Email", c => c.String());
            AlterColumn("dbo.CustomerServices", "Name", c => c.String());
            DropColumn("dbo.CustomerServices", "EditUser");
            DropColumn("dbo.CustomerServices", "EditTime");
            DropColumn("dbo.CustomerServices", "CreateUser");
            DropColumn("dbo.CustomerServices", "CreateTime");
            DropColumn("dbo.CustomerServices", "CustomerServiceId");
            AddPrimaryKey("dbo.CustomerServices", "Id");
        }
    }
}
