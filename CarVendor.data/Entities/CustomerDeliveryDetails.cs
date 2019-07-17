using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
   public class CustomerDeliveryDetails : TEntity<long>
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int Individually { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string DeliveryAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int? PaymethodTypeId { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual CardInfo CardInfo { get; set; }
        public virtual BankTransferInfo BankTransferInfo { get; set; }
        public virtual PaymethodType PaymethodType { get; set; }
    }
}
