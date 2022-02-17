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
    public class DepartmentController : Controller
    {
        IDepartment DepartmentService;

        public DepartmentController()
        {
            this.DepartmentService = new DepartmentService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDepartments()
        {
            List<DepartmentModel> departments = DepartmentService.GetDepartments();
            return Json(departments);
        }
    }
}
