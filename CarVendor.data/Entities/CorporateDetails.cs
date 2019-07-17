using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class CorporateDetails :TEntity<long>
    {

        public string OrgnizationName { get; set; }
        public string OrgnizationSite { get; set; }
        public string RegistrationNo { get; set; }
        [ForeignKey("Id")]
        public virtual CustomerDeliveryDetails UserDeliveryDetails { get; set; }
    }
}
