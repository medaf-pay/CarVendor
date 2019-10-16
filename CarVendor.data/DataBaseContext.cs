﻿using CarVendor.data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data
{
    public class DataBaseContext : DbContext
    {
        static DataBaseContext()
        {
            Database.SetInitializer<DataBaseContext>(null);
        }
        public DataBaseContext()
                   : base("name=CarVendorDb")
        {

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<CarColor> CarColors { get; set; }
        public DbSet<CardInfo> CardsInfo   { get; set; }
        public DbSet<CorporateDetails> CorporatesDetails { get; set; }
        public DbSet<User> Users   { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<PaymethodType> paymethodTypes { get; set; }
        public DbSet<BankTransferInfo> BanksTransferInfo { get; set; }
        public DbSet<CarFamily> CarFamilies { get; set; }
        public DbSet<Mail> Mails { get; set; }
    }
}
