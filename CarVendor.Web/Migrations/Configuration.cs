namespace CarVendor.Web.Migrations
{
    using CarVendor.data;
    using CarVendor.data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarVendor.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarVendor.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            DataBaseContext db = new DataBaseContext();


            List<CarImage> ci = new List<CarImage>()
                {
                    new CarImage()
                    {
                        ImageURL="page3_img3.png"
                    },
                    new CarImage()
                    {
                        ImageURL="page3_img4.png"
                    },
                    new CarImage()
                    {
                        ImageURL="page3_img5.png"
                    }
                };

            List<CarImage> ci1 = new List<CarImage>()
                {
                    new CarImage()
                    {
                        ImageURL="page3_img3.png"
                    },
                    new CarImage()
                    {
                        ImageURL="page3_img4.png"
                    },
                    new CarImage()
                    {
                        ImageURL="page3_img5.png"
                    }
                };

            Brand brand = new Brand()
            {
                Name = "brand1"
            };

            CarFamily carfamily = new CarFamily()
            {
                Name = "carfamily"
            };

            Car car = new Car
            {
                Name = "lancer",
                Model = "2009",
                Condition = CarCondition.New,
                Brand = brand,
                Type = carfamily
            };

            Car car1 = new Car
            {
                Name = "Hondaa",
                Model = "2005",
                Condition = CarCondition.Used,
                Brand = brand,
                Type = carfamily
            };


            Category cat1 = new Category()
            {
                Name = "cat1"
            };

            Category cat2 = new Category()
            {
                Name = "cat2"
            };

            CarCategory carcategory = new CarCategory()
            {
                Car = car,
                Category = cat1,
            };

            CarCategory carcategory1 = new CarCategory()
            {
                Car = car1,
                Category = cat2,
            };


            CarColor carcolor = new CarColor()
            {
                Price = 12345678,
                Discount=70000,
                Quantity = 4,
                CarCategory = carcategory,
                Color = new Color() { Name = "red" },
                CarImages = ci
            };

            CarColor carcolor1 = new CarColor()
            {
                Price = 12345678,
                Quantity = 4,
                CarCategory = carcategory1,
                Color = new Color() { Name = "green" },
                CarImages = ci1
            };


            db.CarColors.Add(carcolor);
            db.CarColors.Add(carcolor1);
            db.SaveChanges();
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
