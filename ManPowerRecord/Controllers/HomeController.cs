using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using ManPowerRecord.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Controllers
{
    public class HomeController : Controller
    {
        IWorkingHours WorkingHoursService;

        public HomeController()
        {
            this.WorkingHoursService = new WorkingHoursService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetWorkingHours()
        {
            List<WorkingHoursModel> whs = WorkingHoursService.GetWorkingHours();
            whs = whs.OrderByDescending(w => w.working_date).ToList();
            return Json(whs);
        }

        [HttpPost]
        public JsonResult AddWorkingHours(string wh_string)
        {
            WorkingHoursModel wh = JsonConvert.DeserializeObject<WorkingHoursModel>(wh_string);
            var result = WorkingHoursService.AddWorkingHours(wh);
            return Json(result);
        }

        [HttpPatch]
        public JsonResult EditWorkingHours(string wh_string)
        {
            WorkingHoursModel wh = JsonConvert.DeserializeObject<WorkingHoursModel>(wh_string);
            return Json("Done");
        }

        public List<EventModel> GetHolidays()
        {
            List<EventModel> holidays = new List<EventModel>();
            return holidays;
        }

        public IActionResult Test()
        {
            ViewData["Title"] = "Test";
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
