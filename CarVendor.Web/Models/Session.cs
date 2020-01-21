using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.Web.Models
{
    public class Session
    {
        public string aes256Key { get; set; }
        public int authenticationLimit { get; set; }
        public string id { get; set; }
        public string updateStatus { get; set; }
        public string version { get; set; }
    }

    public class ResponceSession
    {
        public string merchant { get; set; }
        public string result { get; set; }
        public Session session { get; set; }
    }
}