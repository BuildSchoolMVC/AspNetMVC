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
                (
                x => x.RoomTypeId,
                new RoomType { RoomTypeId = 0, Name = "廚房", Value = 0 },
                new RoomType { RoomTypeId = 1, Name = "客廳", Value = 1 },
                new RoomType { RoomTypeId = 2, Name = "臥室", Value = 2 },
                new RoomType { RoomTypeId = 3, Name = "浴廁", Value = 3 },
                new RoomType { RoomTypeId = 4, Name = "陽台", Value = 4 }
            );
            context.ServiceItems.AddOrUpdate
                (
                x => x.ServiceitemId,
                new ServiceItem { ServiceitemId = 0, Name = "清潔", Value = 0 },
                new ServiceItem { ServiceitemId = 1, Name = "收納", Value = 1 },
                new ServiceItem { ServiceitemId = 2, Name = "除蟲", Value = 2 }
                );
            context.SquareFeets.AddOrUpdate(

                x => x.SquareFeetId,
                new SquareFeet { SquareFeetId = 0, Name = "5坪以下", Value = 0 },
                new SquareFeet { SquareFeetId = 1, Name = "6-10坪", Value = 1 },
                new SquareFeet { SquareFeetId = 2, Name = "11-15坪", Value = 2 },
                new SquareFeet { SquareFeetId = 3, Name = "16坪以上", Value = 3 }
                );
            context.SingleProducts.AddOrUpdate(
                x => x.ProductId,
                new SingleProduct { ProductId = "K01", Name = "廚房清潔", RoomType = 0, Squarefeet = 0, Hour = 1, Price = 500, PhotoUrl = "HQwLxRh.jpg" },
                new SingleProduct { ProductId = "K02", Name = "廚房清潔", RoomType = 0, Squarefeet = 1, Hour = 1.5F, Price = 750, PhotoUrl = "HQwLxRh.jpg" },
                new SingleProduct { ProductId = "K03", Name = "廚房清潔", RoomType = 0, Squarefeet = 2, Hour = 2, Price = 1000, PhotoUrl = "HQwLxRh.jpg" },
                new SingleProduct { ProductId = "K04", Name = "廚房清潔", RoomType = 0, Squarefeet = 3, Hour = 2.5F, Price = 1250, PhotoUrl = "HQwLxRh.jpg" },
                new SingleProduct { ProductId = "L01", Name = "客廳清潔", RoomType = 1, Squarefeet = 0, Hour = 1, Price = 500, PhotoUrl = "Tvcj3OR.jpg" },
                new SingleProduct { ProductId = "L02", Name = "客廳清潔", RoomType = 1, Squarefeet = 1, Hour = 1.5F, Price = 750, PhotoUrl = "Tvcj3OR.jpg" },
                new SingleProduct { ProductId = "L03", Name = "客廳清潔", RoomType = 1, Squarefeet = 2, Hour = 2, Price = 1000, PhotoUrl = "Tvcj3OR.jpg" },
                new SingleProduct { ProductId = "L04", Name = "客廳清潔", RoomType = 1, Squarefeet = 3, Hour = 2.5F, Price = 1250, PhotoUrl = "Tvcj3OR.jpg" },
                new SingleProduct { ProductId = "Bed01", Name = "臥室清潔", RoomType = 2, Squarefeet = 0, Hour = 1, Price = 500, PhotoUrl = "7hTQ5xR.jpg" },
                new SingleProduct { ProductId = "Bed02", Name = "臥室清潔", RoomType = 2, Squarefeet = 1, Hour = 1.5F, Price = 750, PhotoUrl = "7hTQ5xR.jpg" },
                new SingleProduct { ProductId = "Bed03", Name = "臥室清潔", RoomType = 2, Squarefeet = 2, Hour = 2, Price = 1000, PhotoUrl = "7hTQ5xR.jpg" },
                new SingleProduct { ProductId = "Bed04", Name = "臥室清潔", RoomType = 2, Squarefeet = 3, Hour = 2.5F, Price = 1250, PhotoUrl = "7hTQ5xR.jpg" },
                new SingleProduct { ProductId = "Bath01", Name = "浴廁清潔", RoomType = 3, Squarefeet = 0, Hour = 0.5F, Price = 250, PhotoUrl = "7Z8nhs9.jpg" },
                new SingleProduct { ProductId = "Bath02", Name = "浴廁清潔", RoomType = 3, Squarefeet = 1, Hour = 1, Price = 500, PhotoUrl = "7Z8nhs9.jpg" },
                new SingleProduct { ProductId = "Bath03", Name = "浴廁清潔", RoomType = 3, Squarefeet = 2, Hour = 1.5F, Price = 750, PhotoUrl = "7Z8nhs9.jpg" },
                new SingleProduct { ProductId = "Bath04", Name = "浴廁清潔", RoomType = 3, Squarefeet = 3, Hour = 2F, Price = 1000, PhotoUrl = "7Z8nhs9.jpg" },
                new SingleProduct { ProductId = "Bal01", Name = "陽台清潔", RoomType = 4, Squarefeet = 0, Hour = 0.5F, Price = 250, PhotoUrl = "LewIn3G.jpg" },
                new SingleProduct { ProductId = "Bal02", Name = "陽台清潔", RoomType = 4, Squarefeet = 1, Hour = 1, Price = 500, PhotoUrl = "LewIn3G.jpg" },
                new SingleProduct { ProductId = "Bal03", Name = "陽台清潔", RoomType = 4, Squarefeet = 2, Hour = 1.5F, Price = 750, PhotoUrl = "LewIn3G.jpg" },
                new SingleProduct { ProductId = "Bal04", Name = "陽台清潔", RoomType = 4, Squarefeet = 3, Hour = 2F, Price = 1000, PhotoUrl = "LewIn3G.jpg" }
                );
            context.PackageProducts.AddOrUpdate(
                x => x.PackageProductId,
                new PackageProduct { PackageProductId = "P01", Name = "小資消費輕鬆省", RoomType = 2, Squarefeet = 0, RoomType2 = 3, Squarefeet2 = 0, ServiceItem = "清潔+收納", Hour = 1.5F, Price = 750, PhotoUrl = "o2VD8aZ.jpg" },
                new PackageProduct { PackageProductId = "P02", Name = "單身貴族偷偷懶", RoomType = 2, Squarefeet = 1, RoomType2 = 3, Squarefeet2 = 0, ServiceItem = "清潔+收納", Hour = 2, Price = 1000, PhotoUrl = "JL2pg7G.jpg" },
                new PackageProduct { PackageProductId = "P03", Name = "兩房一廳恰恰好", RoomType = 2, Squarefeet = 2, RoomType2 = 1, Squarefeet2 = 1, RoomType3 = 3, Squarefeet3 = 0, ServiceItem = "清潔+收納+除蟲", Hour = 4, Price = 2000, PhotoUrl = "jcZDt0v.jpg" },
                new PackageProduct { PackageProductId = "P04", Name = "乾乾淨淨小家庭", RoomType = 1, Squarefeet = 1, RoomType2 = 0, Squarefeet2 = 1, RoomType3 = 2, Squarefeet3 = 2, ServiceItem = "清潔+收納+除蟲", Hour = 5, Price = 2500, PhotoUrl = "VRIwGhB.jpg" },
                new PackageProduct { PackageProductId = "P05", Name = "三代同堂大家庭", RoomType = 1, Squarefeet = 3, RoomType2 = 0, Squarefeet2 = 2, RoomType3 = 3, Squarefeet3 = 1, ServiceItem = "清潔+收納+除蟲", Hour = 6, Price = 3000, PhotoUrl = "F4OOvVQ.jpg" },
                new PackageProduct { PackageProductId = "P06", Name = "開放廚房一起淨", RoomType = 1, Squarefeet = 2, RoomType2 = 0, Squarefeet2 = 1, ServiceItem = "清潔+除蟲", Hour = 3.5F, Price = 1750, PhotoUrl = "uWZGaPB.jpg" },
                new PackageProduct { PackageProductId = "P07", Name = "陽台廚房亮晶晶", RoomType = 0, Squarefeet = 1, RoomType2 = 4, Squarefeet2 = 1, ServiceItem = "清潔+除蟲", Hour = 2.5F, Price = 1250, PhotoUrl = "k19BwYr.jpg" },
                new PackageProduct { PackageProductId = "P08", Name = "客廳臥室最整齊", RoomType = 1, Squarefeet = 2, RoomType2 = 2, Squarefeet2 = 1, ServiceItem = "清潔+收納", Hour = 3.5F, Price = 1750, PhotoUrl = "qf87eZM.jpg" },
                new PackageProduct { PackageProductId = "P09", Name = "浴廁陽台小幫手", RoomType = 3, Squarefeet = 1, RoomType2 = 4, Squarefeet2 = 0, ServiceItem = "清潔+除蟲", Hour = 1.5F, Price = 750, PhotoUrl = "HOoGu7u.jpg" }
                );
        }

    }
}

