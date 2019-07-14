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
            #region [Brands]
            Brand BMW = new Brand()
            {
                Name = "BMW"
            };


            Brand Mercedes = new Brand()
            {
                Name = "Mercedes"
            };



            Brand KIA = new Brand()
            {
                Name = "KIA"
            };




            #endregion

            #region [Cars]

            Car CarBMW = new Car()
            {
                Name = "CarBMW",
                Brand = BMW,
                Model = "2019",
                Type = CarType.Hatchback,
                Condition = CarCondition.New
            };


            Car CarBMW1 = new Car()
            {
                Name = "CarBMW1",
                Model = "2019",
                Brand = BMW,
                Type = CarType.Hatchback,
                Condition = CarCondition.New
            };
            #endregion



            #region [Category]

            Category category = new Category()
            {
                Name = "Full Option"
            };

            Category category2 = new Category()
            {
                Name = "Half Option"
            };

            #endregion


            #region [Car Categories]
            List<CarCategory> CarCategories = new List<CarCategory>()
            {

                new CarCategory()
                {
                    Car=CarBMW,
                    Category=category,
                    Price=120,
                },

                new CarCategory()
                {
                    Car=CarBMW,
                    Category=category2,
                    Price =100
                }
            };

            context.CarCategories.AddRange(CarCategories);

            #endregion

            #region [Car Colors]

            List<CarColor> CarColors = new List<CarColor>()
            {
                new CarColor()
                {
                    Car=CarBMW,
                    Color=new Color()
                    {
                        Name ="red"
                    },
                    CarImages=new List<CarImage>()
                    {
                        new CarImage(){ImageURL="page1_img2.png"}
                    }
                },

                new CarColor()
                {
                    Car=CarBMW,
                    Color=new Color()
                    {
                        Name ="black"
                    },
                    CarImages=new List<CarImage>()
                    {
                        new CarImage(){ImageURL="page1_img3.png"}
                    }
                },

            };
            context.CarColors.AddRange(CarColors);
            #endregion

        }
    }
}


//  This method will be called after migrating to the latest version.

//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
//  to avoid creating duplicate seed data.
