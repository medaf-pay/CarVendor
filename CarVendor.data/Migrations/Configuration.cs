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
            List<Car> cars = new List<Car>()
            {
               new Car()
               {
                Name="Car1",
                Model="Sozuki",
                Price=120,
                Condition=CarCondition.New,
                Brand=new Brand()
                {
                    Name="Brand1"
                },
                Type=CarType.Coupe
               },
               new Car()
               {
                Name="Car2",
                Model="Sozuki",
                Price=120,
                Condition=CarCondition.New,
                Brand=new Brand()
                {
                    Name="Brand1"
                },
                Type=CarType.Coupe
               },
               new Car()
               {
                Name="Car3",
                Model="Sozuki",
                Price=120,
                Condition=CarCondition.New,
                Brand=new Brand()
                {
                    Name="Brand1"
                },
                Type=CarType.Coupe
               },
               new Car()
               {
                Name="Car4",
                Model="Sozuki",
                Price=120,
                Condition=CarCondition.New,
                Brand=new Brand()
                {
                    Name="Brand1"
                },
                Type=CarType.Coupe
               },
               new Car()
               {
                Name="Car5",
                Model="totya",
                Price=120,
                Condition=CarCondition.New,
                Brand=new Brand()
                {
                    Name="Brand2"
                },
                Type=CarType.CrossOver
               }
        };
            context.Cars.AddRange(cars);
            context.SaveChanges();
        }
    }
}


//  This method will be called after migrating to the latest version.

//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
//  to avoid creating duplicate seed data.
