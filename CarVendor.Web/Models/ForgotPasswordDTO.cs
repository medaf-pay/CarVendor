using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.Web.Models
{
    public class ForgotPasswordDTO
    {
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string callbackUrl { get; set; }

    }
}