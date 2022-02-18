using ManPowerRecord.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Controllers
{
    public class MonthlyWorkingHoursController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetWorkingHours()
        {
            DateTime date = DateTime.Today;
            int days = DateTime.DaysInMonth(date.Year, date.Month);
            List<WorkingHoursModel> whs = new List<WorkingHoursModel>();
            for (int i = 1; i <= days; i++)
            {
                WorkingHoursModel wh = new WorkingHoursModel()
                {
                    working_date = new DateTime(date.Year, date.Month, i),
                };
                whs.Add(wh);
            }
            return Json(whs);
        }
    }
}
