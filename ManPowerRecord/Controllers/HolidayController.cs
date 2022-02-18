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
    public class HolidayController : Controller
    {
        IHoliday HolidayService;

        public HolidayController()
        {
            this.HolidayService = new HolidayService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetHolidays()
        {
            List<HolidayModel> holidays = HolidayService.GetHolidays();
            return Json(holidays);
        }
    }
}
