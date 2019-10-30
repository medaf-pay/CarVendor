using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarVendor.data.Entities
{
    public class Video : TEntity<long>
    {
        public string Link { get; set; }   
        public virtual CarColor CarColor { get; set; }
    }
}
