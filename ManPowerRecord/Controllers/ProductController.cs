using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using ManPowerRecord.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Controllers
{
    public class ProductController : Controller
    {
        IProduct ProductService;

        public ProductController()
        {
            this.ProductService = new ProductService();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewProducts()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetBrands()
        {
            List<BrandModel> brands = ProductService.GetBrands();
            return Json(brands);
        }

        [HttpPost]
        public JsonResult AddBrand(string brand_string)
        {
            BrandModel brand = JsonConvert.DeserializeObject<BrandModel>(brand_string);
            var result = ProductService.AddBrand(brand);
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetProducts()
        {
            List<ProductModel> products = ProductService.GetProducts();
            return Json(products);
        }

        [HttpPost]
        public JsonResult AddProduct(string product_string)
        {
            ProductModel product = JsonConvert.DeserializeObject<ProductModel>(product_string);
            var result = ProductService.AddProduct(product);
            return Json(result);
        }
    }
}
