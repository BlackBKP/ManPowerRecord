using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Models
{
    public class ProductModel
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
        public string brand_id { get; set; }
        public int product_price { get; set; }
        public string product_unit { get; set; }
    }
}
