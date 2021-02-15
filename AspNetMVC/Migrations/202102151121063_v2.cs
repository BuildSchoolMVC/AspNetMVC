namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PackageProducts", "RoomType3", c => c.Int());
            AlterColumn("dbo.PackageProducts", "Squarefeet3", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PackageProducts", "Squarefeet3", c => c.Int(nullable: false));
            AlterColumn("dbo.PackageProducts", "RoomType3", c => c.Int(nullable: false));
        }
    }
}
