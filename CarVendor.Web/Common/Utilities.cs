using CarVendor.data;
using CarVendor.data.Entities;
using CarVendor.mvc.Models;
using CarVendor.Web.Dtos;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;

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
                    UnitPrice = orderItem.NewPrice

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

        public  static T CallOutAPI<T>( string URL ,T result)
        {
     
    
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Add("ContentType", "application/json");

            //This is the key section you were missing    
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("merchant.TESTQNBAATEST001:9c6a123857f1ea50830fa023ad8c8d1b");
            string val = System.Convert.ToBase64String(plainTextBytes);
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + val);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.PostAsJsonAsync(URL, result).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                result = response.Content.ReadAsAsync<T>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            
            }
         

         client.Dispose();
        
            
            return result;
        }
    }
}