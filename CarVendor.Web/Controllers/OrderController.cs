using CarVendor.data;
using CarVendor.data.Entities;
using CarVendor.mvc.Common;
using CarVendor.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;

namespace CarVendor.Web.Controllers
{
    public class OrderController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();
        [HttpPost]
        [Route("api/order/CallBackQNB")]
        public IHttpActionResult CallBackQNB(QNBResponceModel data)
        {
            var StrObj = JsonConvert.SerializeObject(data);
            PaymentCallBack callBackObj = new PaymentCallBack();
            callBackObj.ResopnceObject = StrObj;
            db.paymentCallBacks.Add(callBackObj);
            db.SaveChanges();
            Utilities.ChangeOrderStatus(db, data.order.id, data.result);

            return Ok();
        }
      
    }
}
