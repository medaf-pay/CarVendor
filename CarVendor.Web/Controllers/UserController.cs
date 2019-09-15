using CarVendor.data;
using CarVendor.mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
namespace CarVendor.Web.Controllers
{
    public class UserController : ApiController
    {
        DataBaseContext db = new DataBaseContext();
        // GET: api/User
        [HttpGet]
        [Route("api/User/UserInfoDetails")]
        public CustomerInfoModel UserInfoDetails()
        {
            string Email = User.Identity.GetUserName();
            var CustomerInfo = db.Users.Where(c => c.Email == Email).Select(s => new CustomerInfoModel()
            {
                City = s.UserAddresses.Where(c1 => c1.IsDeleted != true).OrderByDescending(o => o.Id).Select(s1 => s1.Address.City).FirstOrDefault(),
                Country = s.UserAddresses.Where(c1 => c1.IsDeleted != true).OrderByDescending(o => o.Id).Select(s1 => s1.Address.Country).FirstOrDefault(),
                DeliveryAddress = s.UserAddresses.Where(c1 => c1.IsDeleted != true).OrderByDescending(o => o.Id).Select(s1 => s1.Address.DeliveryAddress).FirstOrDefault(),
                State = s.UserAddresses.Where(c1 => c1.IsDeleted != true).OrderByDescending(o => o.Id).Select(s1 => s1.Address.State).FirstOrDefault(),
                Zip = s.UserAddresses.Where(c1 => c1.IsDeleted != true).OrderByDescending(o => o.Id).Select(s1 => s1.Address.Zip).FirstOrDefault(),
                MainAddress = s.UserAddresses.Where(c1 => c1.IsDeleted != true).OrderByDescending(o => o.Id).Select(s1 => s1.Address.MainAddress).FirstOrDefault(),

                Email = s.Email,
                FName = s.FName,
                Individually = s.Individually,
                LName = s.LName,
                MName = s.MName,
                NID = s.NationalId,
                Phone = s.Phone,
                Id=s.Id

            }).FirstOrDefault();
            if(CustomerInfo.Individually==2)
            {
                var corporate= db.CorporatesDetails.Where(c => c.user.Id == CustomerInfo.Id).FirstOrDefault();
                CustomerInfo.OrgnizationName = corporate.CorporateName;
                CustomerInfo.OrgnizationSite = corporate.CorporateSite;
                CustomerInfo.RegistrationNo = corporate.RegistrationNo;

            }
            return CustomerInfo;
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
