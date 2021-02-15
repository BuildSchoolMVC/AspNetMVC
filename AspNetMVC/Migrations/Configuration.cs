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
                new RoomType { RoomTypeId = 0, Name = "�p��", Value = 0 },
                new RoomType { RoomTypeId = 1, Name = "���U", Value = 1 },
                new RoomType { RoomTypeId = 2, Name = "�׫�", Value = 2 },
                new RoomType { RoomTypeId = 3, Name = "�D�Z", Value = 3 },
                new RoomType { RoomTypeId = 4, Name = "���x", Value = 4 }
            );
            context.ServiceItems.AddOrUpdate
                (
                x => x.ServiceitemId,
                new ServiceItem { ServiceitemId = 0, Name = "�M��", Value = 0 },
                new ServiceItem { ServiceitemId = 1, Name = "����", Value = 1 },
                new ServiceItem { ServiceitemId = 2, Name = "����", Value = 2 }
                );
            context.SquareFeets.AddOrUpdate(

                x => x.SquareFeetId,
                new SquareFeet { SquareFeetId = 0, Name = "5�W�H�U", Value = 0 },
                new SquareFeet { SquareFeetId = 1, Name = "6-10�W", Value = 1 },
                new SquareFeet { SquareFeetId = 2, Name = "11-15�W", Value = 2 },
                new SquareFeet { SquareFeetId = 3, Name = "16�W�H�W", Value = 3 }
                );
            context.SingleProducts.AddOrUpdate(
                x => x.ProductId,
                new SingleProduct { ProductId = "K01", Name = "�p�вM��", RoomType = 0, Squarefeet = 0, Hour = 1, Price = 500, PhotoUrl = "HQwLxRh.jpg" },
                new SingleProduct { ProductId = "K02", Name = "�p�вM��", RoomType = 0, Squarefeet = 1, Hour = 1.5F, Price = 750, PhotoUrl = "HQwLxRh.jpg" },
                new SingleProduct { ProductId = "K03", Name = "�p�вM��", RoomType = 0, Squarefeet = 2, Hour = 2, Price = 1000, PhotoUrl = "HQwLxRh.jpg" },
                new SingleProduct { ProductId = "K04", Name = "�p�вM��", RoomType = 0, Squarefeet = 3, Hour = 2.5F, Price = 1250, PhotoUrl = "HQwLxRh.jpg" },
                new SingleProduct { ProductId = "L01", Name = "���U�M��", RoomType = 1, Squarefeet = 0, Hour = 1, Price = 500, PhotoUrl = "Tvcj3OR.jpg" },
                new SingleProduct { ProductId = "L02", Name = "���U�M��", RoomType = 1, Squarefeet = 1, Hour = 1.5F, Price = 750, PhotoUrl = "Tvcj3OR.jpg" },
                new SingleProduct { ProductId = "L03", Name = "���U�M��", RoomType = 1, Squarefeet = 2, Hour = 2, Price = 1000, PhotoUrl = "Tvcj3OR.jpg" },
                new SingleProduct { ProductId = "L04", Name = "���U�M��", RoomType = 1, Squarefeet = 3, Hour = 2.5F, Price = 1250, PhotoUrl = "Tvcj3OR.jpg" },
                new SingleProduct { ProductId = "Bed01", Name = "�׫ǲM��", RoomType = 2, Squarefeet = 0, Hour = 1, Price = 500, PhotoUrl = "7hTQ5xR.jpg" },
                new SingleProduct { ProductId = "Bed02", Name = "�׫ǲM��", RoomType = 2, Squarefeet = 1, Hour = 1.5F, Price = 750, PhotoUrl = "7hTQ5xR.jpg" },
                new SingleProduct { ProductId = "Bed03", Name = "�׫ǲM��", RoomType = 2, Squarefeet = 2, Hour = 2, Price = 1000, PhotoUrl = "7hTQ5xR.jpg" },
                new SingleProduct { ProductId = "Bed04", Name = "�׫ǲM��", RoomType = 2, Squarefeet = 3, Hour = 2.5F, Price = 1250, PhotoUrl = "7hTQ5xR.jpg" },
                new SingleProduct { ProductId = "Bath01", Name = "�D�Z�M��", RoomType = 3, Squarefeet = 0, Hour = 0.5F, Price = 250, PhotoUrl = "7Z8nhs9.jpg" },
                new SingleProduct { ProductId = "Bath02", Name = "�D�Z�M��", RoomType = 3, Squarefeet = 1, Hour = 1, Price = 500, PhotoUrl = "7Z8nhs9.jpg" },
                new SingleProduct { ProductId = "Bath03", Name = "�D�Z�M��", RoomType = 3, Squarefeet = 2, Hour = 1.5F, Price = 750, PhotoUrl = "7Z8nhs9.jpg" },
                new SingleProduct { ProductId = "Bath04", Name = "�D�Z�M��", RoomType = 3, Squarefeet = 3, Hour = 2F, Price = 1000, PhotoUrl = "7Z8nhs9.jpg" },
                new SingleProduct { ProductId = "Bal01", Name = "���x�M��", RoomType = 4, Squarefeet = 0, Hour = 0.5F, Price = 250, PhotoUrl = "LewIn3G.jpg" },
                new SingleProduct { ProductId = "Bal02", Name = "���x�M��", RoomType = 4, Squarefeet = 1, Hour = 1, Price = 500, PhotoUrl = "LewIn3G.jpg" },
                new SingleProduct { ProductId = "Bal03", Name = "���x�M��", RoomType = 4, Squarefeet = 2, Hour = 1.5F, Price = 750, PhotoUrl = "LewIn3G.jpg" },
                new SingleProduct { ProductId = "Bal04", Name = "���x�M��", RoomType = 4, Squarefeet = 3, Hour = 2F, Price = 1000, PhotoUrl = "LewIn3G.jpg" }
                );
            context.PackageProducts.AddOrUpdate(
                x => x.PackageProductId,
                new PackageProduct { PackageProductId = "P01", Name = "�p����O���P��", RoomType = 2, Squarefeet = 0, RoomType2 = 3, Squarefeet2 = 0, ServiceItem = "�M��+����", Hour = 1.5F, Price = 750, PhotoUrl = "o2VD8aZ.jpg" },
                new PackageProduct { PackageProductId = "P02", Name = "�樭�Q�ڰ����i", RoomType = 2, Squarefeet = 1, RoomType2 = 3, Squarefeet2 = 0, ServiceItem = "�M��+����", Hour = 2, Price = 1000, PhotoUrl = "JL2pg7G.jpg" },
                new PackageProduct { PackageProductId = "P03", Name = "��Ф@�U���n", RoomType = 2, Squarefeet = 2, RoomType2 = 1, Squarefeet2 = 1, RoomType3 = 3, Squarefeet3 = 0, ServiceItem = "�M��+����+����", Hour = 4, Price = 2000, PhotoUrl = "jcZDt0v.jpg" },
                new PackageProduct { PackageProductId = "P04", Name = "�����b�b�p�a�x", RoomType = 1, Squarefeet = 1, RoomType2 = 0, Squarefeet2 = 1, RoomType3 = 2, Squarefeet3 = 2, ServiceItem = "�M��+����+����", Hour = 5, Price = 2500, PhotoUrl = "VRIwGhB.jpg" },
                new PackageProduct { PackageProductId = "P05", Name = "�T�N�P��j�a�x", RoomType = 1, Squarefeet = 3, RoomType2 = 0, Squarefeet2 = 2, RoomType3 = 3, Squarefeet3 = 1, ServiceItem = "�M��+����+����", Hour = 6, Price = 3000, PhotoUrl = "F4OOvVQ.jpg" },
                new PackageProduct { PackageProductId = "P06", Name = "�}��p�Ф@�_�b", RoomType = 1, Squarefeet = 2, RoomType2 = 0, Squarefeet2 = 1, ServiceItem = "�M��+����", Hour = 3.5F, Price = 1750, PhotoUrl = "uWZGaPB.jpg" },
                new PackageProduct { PackageProductId = "P07", Name = "���x�p�ЫG����", RoomType = 0, Squarefeet = 1, RoomType2 = 4, Squarefeet2 = 1, ServiceItem = "�M��+����", Hour = 2.5F, Price = 1250, PhotoUrl = "k19BwYr.jpg" },
                new PackageProduct { PackageProductId = "P08", Name = "���U�׫ǳ̾��", RoomType = 1, Squarefeet = 2, RoomType2 = 2, Squarefeet2 = 1, ServiceItem = "�M��+����", Hour = 3.5F, Price = 1750, PhotoUrl = "qf87eZM.jpg" },
                new PackageProduct { PackageProductId = "P09", Name = "�D�Z���x�p����", RoomType = 3, Squarefeet = 1, RoomType2 = 4, Squarefeet2 = 0, ServiceItem = "�M��+����", Hour = 1.5F, Price = 750, PhotoUrl = "HOoGu7u.jpg" }
                );
        }

    }
}

