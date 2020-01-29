using CarVendor.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.mvc.Models
{
    public class CartModel
    {public CartModel()
        {
            CartItems = new List<CartItemModel>();
            CustomerInfo = new CustomerInfoModel();
        }
        public string UserId { get; set; }
        public List<CartItemModel> CartItems { get; set; }
        public CustomerInfoModel CustomerInfo { get; set; }
        public string Guid { get; set; }

        public Order Order { get; set; }
    }
}