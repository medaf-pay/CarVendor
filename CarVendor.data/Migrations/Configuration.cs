namespace CarVendor.data.Migrations
{
    using CarVendor.data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarVendor.data.DataBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarVendor.data.DataBaseContext context)
        {
            Car car1 = new Car()
            {
                Name = "yaris",
                Brand = new Brand() { Name = "toyta" },
                Model = "2011"
            };

            Car car2 = new Car()
            {
                Name = "Krsttala",
                Brand = new Brand() { Name = "Lancer" },
                Model = "2011"
            };

            Category category = new Category()
            {
                Name = "Full Option"
            };

            Category category2 = new Category()
            {
                Name = "Half Option"
            };

            List<CarCategory> CarCategories = new List<CarCategory>()
            {

                new CarCategory()
                {
                    Car=car1,
                    Category=category,
                    Price=120,
                },

                new CarCategory()
                {
                    Car=car2,
                    Category=category2,
                    Price =100
                }

            };

            context.CarCategories.AddRange(CarCategories);

            List<CarColor> CarColors = new List<CarColor>()
            {
                new CarColor()
                {
                    Car=car1,
                    Color=new Color()
                    {
                        Name ="red"
                    }
                }

            };
            context.CarColors.AddRange(CarColors);
        }
    }
}


//  This method will be called after migrating to the latest version.

//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
//  to avoid creating duplicate seed data.
