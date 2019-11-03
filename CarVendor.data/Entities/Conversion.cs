using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
   public  class Conversion :TEntity<long>
    {
     
        public long? FromCurrencyId { get; set; }
        [ForeignKey("FromCurrencyId")]
        public virtual Currency FromCurrency { get; set; }
        
        public long? ToCurrencyId { get; set; }
        [ForeignKey("ToCurrencyId")]
        public virtual Currency ToCurrency { get; set; }
        public DateTime CreationDate { get; set; }
        public  int Value { get; set; }
        public int Plus { get; set; }
    }
}
