using CarVendor.data;
using CarVendor.data.Entities;
using CarVendor.mvc.Models;
using CarVendor.Web.Dtos;
using Microsoft.AspNet.Identity;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CarVendor.mvc.Common
{
    public class Utilities
    {
        public static List<CartModel> _shopingCarts = new List<CartModel>();

        public static List<CurrencyDTO> _currencyDTO = new List<CurrencyDTO>() { new CurrencyDTO() { Code = 1, Name = "EGP",UserIdentity= "default" } };


        public static long SetOrderDetails(DataBaseContext db, bool VIP = false, BankTransferModel BankTransfer = null, string Identity = null, long UserId=0,string Currency="EGP")
        {

            var customer_cart = _shopingCarts.Where(c=>c.UserId== Identity).FirstOrDefault();
            if (customer_cart == null || customer_cart.CustomerInfo == null || customer_cart.CartItems == null || customer_cart.CartItems.Count < 1)
            {
                return -1;
            }

            List<OrderItem> newOrderItems = new List<OrderItem>();
            decimal TotalAmount=0;
            foreach (var orderItem in customer_cart.CartItems)
            {
                TotalAmount += (orderItem.NewPrice*orderItem.Quantity);
                newOrderItems.Add(new OrderItem()
                {
                    CarId = orderItem.CarId,
                    Color = orderItem.Color.Text,
                    Quantity = (int)orderItem.Quantity,
                    Category = orderItem.Category.Text,
                    IsDeleted = false,
                    UnitPrice = orderItem.NewPrice,
                    PaymenType=orderItem.PaymentType==1?"Full Payment":"Down Payment"

                });
            }

            Order newOrder = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = "S" + DateTime.Now.Day.ToString() + "I" + DateTime.Now.Month + "G" + DateTime.Now.Year.ToString().Substring(2, 2),
                OrderItems = newOrderItems,
                TotalAmount = TotalAmount,
                Currency = Currency,
            IsDeleted = false,
                UserId = UserId,
               Status= "Pending"

            };
            customer_cart.Order = newOrder;
     
     
       
            customer_cart.CustomerInfo = db.Users.Where(s => s.Id == UserId).Select(u => new CustomerInfoModel
            {
                Individually = u.Individually,
                FName = u.FName,
                MName = u.MName,
                LName = u.LName,
                Phone = u.Phone,
                DeliveryAddress = u.UserAddresses.Where(a => a.User.Id == UserId).FirstOrDefault().Address.DeliveryAddress,
                MainAddress = u.UserAddresses.Where(a => a.User.Id == UserId).FirstOrDefault().Address.MainAddress

                //DeliveryAddress=u.UserAddresses,
            }).FirstOrDefault();
            db.Orders.Add(newOrder);
            // TODO: Send Mail Here
            if (VIP)
            {
                EmailTemplate Email = new EmailTemplate();
                string path = @"~/Common/OrderDetailsEmailTemplate.html";
                var emailHtml = Email.ReadTemplateEmail(customer_cart, path);
                GmailSender.SendEmail("mpay.services@medafinvestment.com", "Serious!1", db.Mails.Select(s => s.mail).ToList(), "Order", emailHtml, null);
            }
            try
            {
                db.SaveChanges();
                BankTransferInfo bankTransferInfo = null;
                if (BankTransfer != null)
                {
                    bankTransferInfo = new BankTransferInfo()
                    {
                        ACH = BankTransfer.ACH,
                        BBranch = BankTransfer.BBranch,
                        BName = BankTransfer.BName,
                        InputReferenceNo = BankTransfer.InputReferenceNo,
                        Memo = BankTransfer.Memo,
                        PaymentDate = BankTransfer.PaymentDate,
                        TransferNo = BankTransfer.TransferNo,
                        OrderId = newOrder.Id
                    };
                    db.BanksTransferInfo.Add(bankTransferInfo);
                    db.SaveChanges();
                }
              
            }
            catch (Exception ex)
            {

                throw new Exception("error", ex);

            }
        

            return newOrder.Id;

        }

        public static void ChangeOrderStatus(DataBaseContext db, CarVendor.Web.Models.Order ROrder, string status)
        {
            Order order = db.Orders.Where(c => c.Id == ROrder.id).FirstOrDefault();
            var exchange = db.Conversions.Where(cc => cc.FromCurrency.Name == ROrder.currency).OrderByDescending(o => o.CreationDate).Select(s => s.Value).FirstOrDefault();

            switch (ROrder.currency)
            {
                case "USD":
                 if(order.Currency=="USD" && order.TotalAmount== (decimal)ROrder.amount)
                    {
                        order.Status = status;
                       
                    }
                    else
                    {
                        order.Status = "Different Amount";
                    }
                   
                    break;
                case "EUR":
                    if (order.Currency == "EUR" && order.TotalAmount == (decimal)ROrder.amount)
                    {
                        order.Status = status;
                    }
                    else
                    {
                        order.Status = "Different Amount";
                    }
                    break;
                default:
                    if (order.Currency == "EGP" && order.TotalAmount == (decimal)ROrder.amount)
                    {
                        order.Status = status;
                    }
                    else
                    {
                        order.Status = "Different Amount";
                    }
                    break;
            }
            
            db.SaveChanges();
            CartModel customer_cart = new CartModel();
            customer_cart.Order = order;
            customer_cart.CartItems = order.OrderItems.Select(s=> new CartItemModel
            {
                Brand = s.Car.Brand.Name,
                CarName = s.Car.Name,
                Category = new CategoryModel { Text = s.Category },
                Color = new ColorModel { Text = s.Color },
                Price = s.TotalPrice,
                Quantity = s.Quantity
            } ).ToList();
 
            customer_cart.CustomerInfo = db.Users.Where(s => s.Id == order.UserId).Select(u => new CustomerInfoModel
            {
                Individually = u.Individually,
                FName = u.FName,
                MName = u.MName,
                LName = u.LName,
                Phone = u.Phone,
                DeliveryAddress = u.UserAddresses.Where(a => a.User.Id == order.UserId).FirstOrDefault().Address.DeliveryAddress,
                MainAddress = u.UserAddresses.Where(a => a.User.Id == order.UserId).FirstOrDefault().Address.MainAddress

                //DeliveryAddress=u.UserAddresses,
            }).FirstOrDefault();
    
            // TODO: Send Mail Here
              EmailTemplate Email = new EmailTemplate();
              string path = @"~/Common/OrderDetailsEmailTemplate.html";
               var emailHtml = Email.ReadTemplateEmail(customer_cart, path);
            var Emails = db.Mails.Select(s => s.mail).ToList();
         
            try
            {
                Emails.Add(order.User.Email);
                GmailSender.SendEmail("mpay.services@medafinvestment.com", "Serious!1", Emails, "Order", emailHtml, null);
          

            }
            catch (Exception ex)
            {

                throw new Exception("error", ex);

            }


           

        }

        public async static Task<T> CallOutAPI<T>( string URL ,T result)
        {
     
    


            var client = new RestClient(URL);
            client.Authenticator = new HttpBasicAuthenticator("Merchant.MODERNMOTORS", "bb078bb07b5102fbe9589ab82378c999");

            var request = new RestRequest(Method.POST);

            var response = client.Execute<T>(request);
        if (response.IsSuccessful)
            {
                // Parse the response body.
               result =  response.Data;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            
            }
         

        
        
            
            return result;
        }
        public static List<CartItemModel> SetCartItem(DataBaseContext db,string Identity,decimal ExchangeRate, List<CartItemModel> CartItems)
        {
            CartItemModel car;
            List<CartItemModel> items = new List<CartItemModel>();
            CurrencyDTO currency = _currencyDTO.Where(c => c.UserIdentity ==Identity).Select(s => new CurrencyDTO { Code = s.Code, Name = s.Name }).FirstOrDefault();

            foreach (var item in CartItems)
            {
                if (items.Count > 0 && items.Any(c => c.CarId == item.CarId && c.Category.Id == item.Category.Id && c.Color.Id == item.Color.Id))
                {
                    items.Where(c => c.CarId == item.CarId && c.Category.Id == item.Category.Id && c.Color.Id == item.Color.Id).Select(s => { s.Quantity = s.Quantity + 1; return s; }).ToList();
                    continue;
                }


                car = new CartItemModel();
                car = db.Cars.Where(c => c.Id == item.CarId/* && c.Carcategories.Any(g=>g.CategoryId==item.Category.Id) && c.CarColors.Any(g => g.ColorId == item.Color.Id)*/).Select(s =>
                     new CartItemModel
                     {
                         Brand = s.Brand.Name,
                         CarId = s.Id,
                         CarName = s.Name,
                         PaymentType=item.PaymentType,
                         Category = s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => new CategoryModel { Id = s1.Category.Id, Text = s1.Category.Name }).FirstOrDefault(),
                         Color = s.Carcategories.Where(c => c.CategoryId == item.Category.Id).
                         Select(s2 => s2.CarColors.Where(c => c.ColorId == item.Color.Id).
                         Select(s1 => new ColorModel { Id = s1.Color.Id, Text = s1.Color.Name, Price = (s1.Price / ExchangeRate), NewPrice = ((s1.Price / ExchangeRate) - (s1.Discount / ExchangeRate)) }).FirstOrDefault()).FirstOrDefault(),
                         Price = (s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => s1.CarColors.Select(s2 => s2.Price).FirstOrDefault()).FirstOrDefault()) / ExchangeRate,
                         NewPrice = (s.Carcategories.Where(c => c.CategoryId == item.Category.Id).Select(s1 => s1.CarColors.Select(s2 => s2.Price - s2.Discount).FirstOrDefault()).FirstOrDefault()) / ExchangeRate,

                         Quantity = item.Quantity == 0 ? 1 : item.Quantity,


                     }).FirstOrDefault();
                car.Currency = currency;
                if(item.PaymentType==2)
                {
                    car.Price = 10000 / ExchangeRate;
                    car.NewPrice = 10000 / ExchangeRate;
                    car.Color.NewPrice = 10000 / ExchangeRate;
                    car.Color.Price = 10000 / ExchangeRate;

                }
                var g = car.Color.Price.ToString().IndexOf('.');
                car.Color.Price =decimal.Parse(car.Color.Price.ToString().Substring(0,car.Color.Price.ToString().IndexOf('.')!=-1? car.Color.Price.ToString().IndexOf('.') + 3: car.Color.Price.ToString().Length)) ;
                car.Price =decimal.Parse(car.Price.ToString().Substring(0,car.Price.ToString().IndexOf('.') != -1 ? car.Price.ToString().IndexOf('.') + 3 : car.Price.ToString().Length )) ;
                car.Color.NewPrice = decimal.Parse(car.Color.NewPrice.ToString().Substring(0, car.Color.NewPrice.ToString().IndexOf('.') != -1 ? car.Color.NewPrice.ToString().IndexOf('.') + 3 : car.Color.NewPrice.ToString().Length ));
                car.NewPrice = decimal.Parse(car.NewPrice.ToString().Substring(0, car.NewPrice.ToString().IndexOf('.') != -1 ? car.NewPrice.ToString().IndexOf('.') + 3 : car.NewPrice.ToString().Length ));
              
          

                items.Add(car);
            }
            return items;
        }
    }

}