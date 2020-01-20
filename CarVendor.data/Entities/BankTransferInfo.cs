using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
  public  class BankTransferInfo
    {
        public long Id { get; set; }
        public string BName { get; set; }
        public string BBranch { get; set; }
        public string PaymentDate { get; set; }
        public string TransferNo { get; set; }
        public string InputReferenceNo { get; set; }
        public string ACH { get; set; }
        public string Memo { get; set; }
        public long OrderId { get; set; }
    }
}
