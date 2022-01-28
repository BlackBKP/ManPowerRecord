using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Interfaces
{
    interface IProduct
    {
        List<BrandModel> GetBrands();
        List<ProductModel> GetProducts();
    }
}
