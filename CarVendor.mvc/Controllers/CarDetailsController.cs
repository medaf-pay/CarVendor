using CarVendor.data;
using CarVendor.mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarVendor.mvc.ViewModels;
using CarVendor.data.Entities;
using CarVendor.mvc.Dtos;

namespace CarVendor.mvc.Controllers
{

    public class CarDetailsController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();
        [HttpGet]
        [Route("api/CarDetails/GetImageByColorId/{carId}/{colorId}")]
        public IHttpActionResult GetImageByColorId(long carId, long colorId)
        {
            var image = db.CarColors.Where(c => c.CarId == carId & c.ColorId == colorId).Select(s => s.CarImages.Select(s1 => s1.ImageURL).FirstOrDefault()).FirstOrDefault();
            return Ok("/images/" + image);
        }

        [HttpGet]
        [Route("api/CarDetails/GetPriceCategoryId/{carId}/{categoryId}")]
        public IHttpActionResult GetPriceCategoryId(long carId, long categoryId)
        {
            var Price = db.CarCategories.Where(c => c.CarId == carId && c.CategoryId == categoryId).Select(s => s.Price).FirstOrDefault();
            return Ok(Price);
        }

        [HttpGet]
        [Route("api/CarDetails/CartData")]
        public IHttpActionResult CartData(string SessionId)
        {
            var userCart = HomeController._shopingCarts.FirstOrDefault(cart => cart.SessionId == SessionId);
            if (userCart == null)
                return Ok(NotFound());
            List<CartItemModel> Cars = new List<CartItemModel>();
            CartItemModel car;
            foreach (var item in userCart.CartItems)
            {
                if (Cars.Any(c => c.CarId == item.CarId && c.Category.Id == item.Category.Id && c.Color.Id == item.Color.Id))
                {
                    Cars.Where(c => c.CarId == item.CarId && c.Category.Id == item.Category.Id && c.Color.Id == item.Color.Id).Select(s => { s.Quantity = s.Quantity + 1; return s; }).ToList();
                    continue;
                }
                car = new CartItemModel();
                car = db.Cars.Where(c => c.Id == item.CarId/* && c.Carcategories.Any(g=>g.CategoryId==item.Category.Id) && c.CarColors.Any(g => g.ColorId == item.Color.Id)*/).Select(s =>
                     new CartItemModel
                     {
                         Brand = s.Brand.Name,
                         CarId = s.Id,
                         CarName = s.Name,
                         Category = s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => new CategoryModel { Id = s1.Category.Id, Text = s1.Category.Name }).FirstOrDefault(),
                         Color = s.CarColors.Where(c => c.ColorId == item.Color.Id).Select(s1 => new ColorModel { Id = s1.Color.Id, Text = s1.Color.Name }).FirstOrDefault(),
                         Price = s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => s1.Price).FirstOrDefault(),
                         Quantity = 1
                     }).FirstOrDefault();
                Cars.Add(car);
            }
            return Ok(Cars);
        }

        [HttpPost]
        [Route("api/CartDetails/SetFinalItems")]
        public IHttpActionResult SetFinalItems(string SessionId, List<CartItemModel> Items)
        {
            HomeController._shopingCarts.FirstOrDefault(cart => cart.SessionId == SessionId).CartItems = Items;
            return Ok();

            // return View(userCart.CartItems);
        }

        [HttpGet]
        [Route("api/CartDetails/GetFinalItems")]
        public IHttpActionResult GetFinalItems(string SessionId)
        {
            var Items = HomeController._shopingCarts.FirstOrDefault(cart => cart.SessionId == SessionId).CartItems;
            return Ok(Items);
        }
        [HttpPost]
        [Route("api/CartDetails/Payment")]
        public IHttpActionResult Payment(string SessionId, CustomerInfoModel CustomerInfo)
        {
            var customer_cart = HomeController._shopingCarts.FirstOrDefault(cart => cart.SessionId == SessionId);
            customer_cart.CustomerInfo = CustomerInfo;
            return Ok();
        }
        [HttpPost]
        [Route("api/cartdetails/paybycreditcard")]
        public IHttpActionResult PayCreditCard(string sessionId, CreditCardModel creditCard)
        {
            var customer_cart = HomeController._shopingCarts.FirstOrDefault(cart => cart.SessionId == sessionId);
            if (customer_cart == null && customer_cart.CustomerInfo == null && customer_cart.CartItems == null && customer_cart.CartItems.Count < 1)
                return NotFound();
            List<OrderItem> newOrderItems = new List<OrderItem>();
            foreach (var orderItem in customer_cart.CartItems)
            {
                newOrderItems.Add(new OrderItem()
                {
                    CarId = orderItem.CarId,
                    Color = orderItem.Color.Text,
                    Quantity = (int)orderItem.Quantity,
                    Category = orderItem.Category.Text,
                });
            }
            Order newOrder = new Order()
            {
                Owner = new User()
                {
                    Email = customer_cart.CustomerInfo.Email,
                    Mobile = customer_cart.CustomerInfo.Phone,
                    Name = $"{customer_cart.CustomerInfo.FName} {customer_cart.CustomerInfo.MName} {customer_cart.CustomerInfo.LName}",
                    FirstName = customer_cart.CustomerInfo.FName,
                    MiddleName = customer_cart.CustomerInfo.MName,
                    LastName = customer_cart.CustomerInfo.LName,
                    Address1 = customer_cart.CustomerInfo.Address1,
                    Address2 = customer_cart.CustomerInfo.Address2,
                    City = customer_cart.CustomerInfo.City,
                    Country = customer_cart.CustomerInfo.Country,
                    Individually = customer_cart.CustomerInfo.Individually,
                    OrgnizationName = customer_cart.CustomerInfo.OrgnizationName,
                    OrgnizationSite = customer_cart.CustomerInfo.OrgnizationSite,
                    State = customer_cart.CustomerInfo.State,
                    RegistrationNo = customer_cart.CustomerInfo.RegistrationNo,
                    Zip = customer_cart.CustomerInfo.Zip
                },
                OrderItems = newOrderItems,
                DeliveryDetails = new DeliveryDetails()
                {
                    Address = customer_cart.CustomerInfo.Address1,
                    City = customer_cart.CustomerInfo.City,
                    ContactNumber = customer_cart.CustomerInfo.Phone,
                    ContactPerson = $"{customer_cart.CustomerInfo.FName} {customer_cart.CustomerInfo.MName} {customer_cart.CustomerInfo.LName}",
                    Country = customer_cart.CustomerInfo.Country,
                    Town = customer_cart.CustomerInfo.State
                }
            };
            db.Orders.Add(newOrder);
            db.SaveChanges();

            return Ok(new OrderDto()
            {
                Id = newOrder.Id
            });
        }

        [HttpGet]
        [Route("api/CartDetails/getFilters")]
        public IHttpActionResult getFilters()
        {
            filtersModel filters = new filtersModel();
            filters.Brands = db.Brands.Select(s => new BaseViewModel { Id = s.Id, Name = s.Name }).ToList();
            filters.Categories = db.Categories.Select(s => new BaseViewModel { Id = s.Id, Name = s.Name }).ToList();
            filters.Colors = db.Colors.Select(s => new BaseViewModel { Id = s.Id, Name = s.Name }).ToList();
            return Ok(filters);
        }

        [HttpGet]
        [Route("api/CartDetails/Payment")]
        public IHttpActionResult Payment()
        {
            return Ok();
        }

    }
}
