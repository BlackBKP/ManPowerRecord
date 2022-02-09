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
        string AddBrand(BrandModel brand);
        List<ProductModel> GetProducts();
        string AddProduct(ProductModel product);
    }
}
