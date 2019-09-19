﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class Category : TEntity<long>
    {
        public string Name { get; set; }
        public  virtual ICollection<CarCategory> CarCategories { get; set; }

    }
}
