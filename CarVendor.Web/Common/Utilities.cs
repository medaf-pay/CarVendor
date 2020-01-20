using CarVendor.data;
using CarVendor.data.Entities;
using CarVendor.mvc.Models;
using CarVendor.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarVendor.mvc.Common
{
    public class Utilities
    {
        public static List<CartModel> _shopingCarts = new List<CartModel>();

        public static List<CurrencyDTO> _currencyDTO = new List<CurrencyDTO>() { new CurrencyDTO() { Code = 1, Name = "EGP",UserIdentity= "default" } };
    
      
        public static long SetOrderDetails( DataBaseContext db, bool VIP=false, BankTransferModel BankTransfer=null,long UserId=0)
        {

            var customer_cart = _shopingCarts.FirstOrDefault();
            if (customer_cart == null && customer_cart.CustomerInfo == null && customer_cart.CartItems == null && customer_cart.CartItems.Count < 1)
            {
                return -1;
            }

            List<OrderItem> newOrderItems = new List<OrderItem>();
            foreach (var orderItem in customer_cart.CartItems)
            {
                newOrderItems.Add(new OrderItem()
                {
                    CarId = orderItem.CarId,
                    Color = orderItem.Color.Text,
                    Quantity = (int)orderItem.Quantity,
                    Category = orderItem.Category.Text,
                    IsDeleted = false

                });
            }

            Order newOrder = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = "S" + DateTime.Now.Day.ToString() + "I" + DateTime.Now.Month + "G" + DateTime.Now.Year.ToString().Substring(2, 2),
                OrderItems = newOrderItems,
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
           EmailTemplate Email = new EmailTemplate();
           string path = @"~/Common/OrderDetailsEmailTemplate.html";
           var emailHtml = Email.ReadTemplateEmail(customer_cart, path);
            GmailSender.SendEmail("mpay.services@medafinvestment.com", "Serious!1", db.Mails.Select(s => s.mail).ToList(), "Order", emailHtml, null);
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

        public static long ChangeOrderStatus(DataBaseContext db)
        {

            var customer_cart = _shopingCarts.FirstOrDefault();
            if (customer_cart == null && customer_cart.CustomerInfo == null && customer_cart.CartItems == null && customer_cart.CartItems.Count < 1)
            {
                return -1;
            }

            List<OrderItem> newOrderItems = new List<OrderItem>();
            foreach (var orderItem in customer_cart.CartItems)
            {
                newOrderItems.Add(new OrderItem()
                {
                    CarId = orderItem.CarId,
                    Color = orderItem.Color.Text,
                    Quantity = (int)orderItem.Quantity,
                    Category = orderItem.Category.Text,
                    IsDeleted = false

                });
            }

            Order newOrder = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = "S" + DateTime.Now.Day.ToString() + "I" + DateTime.Now.Month + "G" + DateTime.Now.Year.ToString().Substring(2, 2),
                OrderItems = newOrderItems,
                IsDeleted = false,
                UserId = UserId,
                Status = "Pending"

            };
            customer_cart.Order = newOrder;
            CardInfo card = null;
            if (creditCard != null)
            {
                card = new CardInfo()
                {
                    CardNumber = creditCard.Number,
                    ExpiryDateMonth = creditCard.ExpiryDateMonth,
                    ExpiryDateYear = creditCard.ExpiryDateYear,
                    CVCode = creditCard.CVCode

                };
            }

            //   CorporateDetails corporateDetails = null;

            //  CustomerDeliveryDetails Owner = new CustomerDeliveryDetails()
            //{
            //    Email = customer_cart.CustomerInfo.Email,
            //    Mobile = customer_cart.CustomerInfo.Phone,
            //    FirstName = customer_cart.CustomerInfo.FName,
            //    MiddleName = customer_cart.CustomerInfo.MName,
            //    LastName = customer_cart.CustomerInfo.LName,
            //    Address = customer_cart.CustomerInfo.MainAddress,
            //    DeliveryAddress = customer_cart.CustomerInfo.DeliveryAddress,
            //    City = customer_cart.CustomerInfo.City,
            //    Country = customer_cart.CustomerInfo.Country,
            //    Individually = customer_cart.CustomerInfo.Individually,
            //    PaymethodTypeId = 1,
            //    Zip = customer_cart.CustomerInfo.Zip,
            //    CardInfo = card,
            //    BankTransferInfo = bankTransferInfo

            //};

            //newOrder.DeliveryDetails = Owner;
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
            //   EmailTemplate Email = new EmailTemplate();
            //  string path = @"~/Common/OrderDetailsEmailTemplate.html";
            //   var emailHtml = Email.ReadTemplateEmail(customer_cart, path);
            // GmailSender.SendEmail("mpay.services@medafinvestment.com", "Serious!1", db.Mails.Select(s => s.mail).ToList(), "Order", emailHtml, null);
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


    }
}