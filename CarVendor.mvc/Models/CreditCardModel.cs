using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.mvc.Models
{
    public class CreditCardModel
    {
        public string Number { get; set; }
        public string ExpiryDateMonth { get; set; }
        public string ExpiryDateYear { get; set; }
        public string CVCode { get; set; }
        public decimal TotalPrice { get; set; }
    }
}