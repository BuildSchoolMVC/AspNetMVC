namespace AspNetMVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AspNetMVC.Models.Entity;

    internal sealed class Configuration : DbMigrationsConfiguration<AspNetMVC.Models.UCleanerDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AspNetMVC.Models.UCleanerDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.RoomTypes.AddOrUpdate
                (x => x.RoomTypeId,
                new RoomType { RoomTypeId = 0, Name = "¼p©Ð", Value = 0, CreateTime = DateTime.Now,CreateUser="",EditTime =DateTime.Now,EditUser="" },
                new RoomType
                {
                    RoomTypeId = 0,
                    Name = "¼p©Ð",
                    Value = 1,
                    CreateTime = DateTime.Now,
                    CreateUser = "",
                    EditTime = DateTime.Now,
                    EditUser = ""
                },
                 new RoomType { RoomTypeId = 2, Name = "ª×«Ç", Value = 2, CreateTime = DateTime.Now, CreateUser = "", EditTime = DateTime.Now, EditUser = "" },
                new RoomType { RoomTypeId = 3, Name = "¯D´Z", Value = 3, CreateTime = DateTime.Now, CreateUser = "", EditTime = DateTime.Now, EditUser = "" },
                new RoomType { RoomTypeId = 4, Name = "¶§¥x", Value = 4, CreateTime = DateTime.Now, CreateUser = "", EditTime = DateTime.Now, EditUser = "" }
            );
        }
    }
}
