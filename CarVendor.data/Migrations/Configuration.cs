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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CarVendor.data.DataBaseContext context)
        {

            #region [Brands]
            List<Brand> Brands = new List<Brand>() {
          new Brand()
            {
                Name = "Suzuki"
            },
         new Brand()
            {
                Name = "Nissan"
            },
            new Brand()
            {
                Name = "Changan"
            }
            };
            #endregion

            #region [Colors]
            List<Color> Colors = new List<Color>() {
            new Color()  {Name = "Red" }
            ,new Color() { Name = "White" }
            ,new Color() { Name = "Black" }
            ,new Color() { Name = "Grey" }
            ,new Color() { Name = "Yellow" }
            ,new Color() { Name = "Silver" }
            ,new Color() { Name = "Gold" }
            ,new Color() { Name = "Pepsi" }
        };
            #endregion

            #region [CarFamilies]
            List<CarFamily> CarFamilies = new List<CarFamily>() {
           new CarFamily() { Name =    "Sedan"  },
           new CarFamily() { Name =  "Hatchback"  },
            new CarFamily() { Name =  "SUV"  },
             new CarFamily() { Name =   "Saloon"  },
            new CarFamily() { Name = "Coupe" },
            new CarFamily() { Name = "CrossOver" }
            };
            #endregion

            #region [Categories]
            List<Category> Categories = new List<Category>() {
            new Category() { Name = "Manual" },
             new Category() { Name = "Automatic" },
             new Category() { Name = "Full Options" }
            };
            #endregion

             #region [Cars]

            List<Car> Cars = new List<Car>() {
           new Car {
                Name = "CarBMW",
                BrandId = 1,
                Model = "2019",
                TypeId = 1,
                Condition = CarCondition.New,
                IsDeleted=false,
                Carcategories=new List<CarCategory>()
                {
                    new CarCategory()
                    {
                        CategoryId=1,
                        IsDeleted=false,
                        CarColors=new List<CarColor>()
                        {
                            new CarColor()
                            {
                                ColorId=1,
                                IsDeleted=false,
                                Price=130000,
                                Quantity=2,
                                CarImages=new List<CarImage>()
                                {
                                    new CarImage()
                                    {
                                        ImageURL="SuzukiSiyaz2019.JPG",
                                        IsDeleted=false
                                    }
                                }

                            },
                                 new CarColor()
                            {
                                ColorId=2,
                                IsDeleted=false,
                                Price=131000,
                                Quantity=3,
                                CarImages=new List<CarImage>()
                                {
                                    new CarImage()
                                    {
                                        ImageURL="SuzukiSX42019.JPG",
                                        IsDeleted=false
                                    }
                                }

                            }

                        }
                    },
  new CarCategory()
                    {
                        CategoryId=2,
                        IsDeleted=false,
                        CarColors=new List<CarColor>()
                        {
                            new CarColor()
                            {
                                ColorId=8,
                                IsDeleted=false,
                                Price=130000,
                                Quantity=4,
                                CarImages=new List<CarImage>()
                                {
                                    new CarImage()
                                    {
                                        ImageURL="SuzukiSwiftDzire2019.JPG",
                                        IsDeleted=false
                                    }
                                }

                            },
                                 new CarColor()
                            {
                                ColorId=6,
                                IsDeleted=false,
                                Price=131000,
                                Quantity=1,
                                CarImages=new List<CarImage>()
                                {
                                    new CarImage()
                                    {
                                        ImageURL="SuzukiErtiga2019.JPG",
                                        IsDeleted=false
                                    }
                                }

                            }

                        }
                    }
                }
              }
            };
            #endregion
            
            #region [Context]
            context.Brands.AddRange(Brands);
            context.Colors.AddRange(Colors);
            context.Categories.AddRange(Categories);
            context.CarFamilies.AddRange(CarFamilies);
            context.SaveChanges();
            context.Cars.AddRange(Cars);
            context.SaveChanges();
            #endregion
        }
    }
}
