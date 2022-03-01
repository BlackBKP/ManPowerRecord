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
            List<DepartmentModel> departments = DepartmentService.GetDepartments().OrderBy(o => o.department_id).ToList();
            return Json(departments);
        }

        [HttpPost]
        public JsonResult CreateDepartment(string department_string)
        {
            DepartmentModel department = JsonConvert.DeserializeObject<DepartmentModel>(department_string);
            var result = "Done";
            return Json(result);
        }

        [HttpPatch]
        public JsonResult UpdateDepartment(string department_string)
        {
            DepartmentModel department = JsonConvert.DeserializeObject<DepartmentModel>(department_string);
            var result = "Done";
            return Json(result);
        }
    }
}
