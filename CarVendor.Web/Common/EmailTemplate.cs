using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stubble.Core.Builders;
using System.IO;
using System.Text;
using CarVendor.mvc.Models;

namespace CarVendor.mvc.Common
{
    public class EmailTemplate
    {
        public string ReadTemplateEmail(CartModel model)
        {
            var stubble = new StubbleBuilder().Build();
           
            using (StreamReader streamReader = new StreamReader(HttpContext.Current.Server.MapPath(@"~/Common/EmailTemplate.html"), Encoding.UTF8))

            {
               return stubble.Render(streamReader.ReadToEnd(), model);
                // Do Stuff
            }
        }
    }
}