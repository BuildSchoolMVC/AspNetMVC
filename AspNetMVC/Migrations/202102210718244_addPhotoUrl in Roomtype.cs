namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPhotoUrlinRoomtype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomTypes", "PhotoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomTypes", "PhotoUrl");
        }
    }
}
