using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using ManPowerRecord.Services;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public JsonResult GetProducts()
        {
            List<ProductModel> products = ProductService.GetProducts();
            return Json(products);
        }
    }
}
