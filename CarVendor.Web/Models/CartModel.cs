using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.mvc.Models
{
    public class CartModel
    {
        public string SessionId { get; set; }
        public List<CartItemModel> CartItems { get; set; }
        public CustomerInfoModel CustomerInfo { get; set; }
        public string Guid { get; set; }
    }
}