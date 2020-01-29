using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.Web.Models
{

    public class Order
    {
        public double amount { get; set; }
        public DateTime creationTime { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public string fundingStatus { get; set; }
        public long id { get; set; }
        public string merchantCategoryCode { get; set; }
        public string status { get; set; }
        public double totalAuthorizedAmount { get; set; }
        public double totalCapturedAmount { get; set; }
        public double totalRefundedAmount { get; set; }
    }

    public class QNBResponceModel
    {

        public Order order { get; set; }

        public string result { get; set; }

    }
}
