using CarVendor.data;
using CarVendor.data.Entities;
using CarVendor.mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarVendor.mvc.Common
{
    public class Utilities
    {
        public static List<CartModel> _shopingCarts = new List<CartModel>();
        public static long SetOrderDetails(string sessionId, DataBaseContext db, CreditCardModel creditCard =null, BankTransferModel BankTransfer=null)
        {

            var customer_cart = _shopingCarts.FirstOrDefault(cart => cart.SessionId == sessionId);
            if (customer_cart == null && customer_cart.CustomerInfo == null && customer_cart.CartItems == null && customer_cart.CartItems.Count < 1)
                return -1;
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
                OrderDate = DateTime.Now,
                OrderNumber = DateTime.Now.Day.ToString() + DateTime.Now.Month + DateTime.Now.Year,
                OrderItems = newOrderItems
            };
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
                    TransferNo = BankTransfer.TransferNo
                };

            }
            CorporateDetails corporateDetails = null;

            CustomerDeliveryDetails Owner = new CustomerDeliveryDetails()
            {
                Email = customer_cart.CustomerInfo.Email,
                Mobile = customer_cart.CustomerInfo.Phone,
                FirstName = customer_cart.CustomerInfo.FName,
                MiddleName = customer_cart.CustomerInfo.MName,
                LastName = customer_cart.CustomerInfo.LName,
                Address = customer_cart.CustomerInfo.Address1,
                DeliveryAddress = customer_cart.CustomerInfo.Address2,
                City = customer_cart.CustomerInfo.City,
                Country = customer_cart.CustomerInfo.Country,
                Individually = customer_cart.CustomerInfo.Individually,
                PaymethodTypeId = 1,
                Zip = customer_cart.CustomerInfo.Zip,
                CardInfo = card,
                BankTransferInfo = bankTransferInfo

            };

            newOrder.DeliveryDetails = Owner;
            db.Orders.Add(newOrder);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            if (customer_cart.CustomerInfo.Individually == 2)
            {
                corporateDetails = new CorporateDetails()
                {
                    OrgnizationName = customer_cart.CustomerInfo.OrgnizationName,
                    OrgnizationSite = customer_cart.CustomerInfo.OrgnizationSite,
                    RegistrationNo = customer_cart.CustomerInfo.RegistrationNo,
                    Id = newOrder.DeliveryDetailsId
                };
                db.corporateDetails.Add(corporateDetails);
                db.SaveChanges();
            }

            return newOrder.Id;
            
        }
    }
}