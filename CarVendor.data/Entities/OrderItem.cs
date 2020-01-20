﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class OrderItem:TEntity<long>
    {
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
   
        public int Quantity { get; set; }
        public decimal TotalPrice { get { return UnitPrice * Quantity; } set { } }
        public string Category { get; set; }
        #region [ Car ]
        public long CarId { get; set; }
        public virtual Car Car { get; set; }
        #endregion

        #region [ Order ]
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
        #endregion
    }
}
