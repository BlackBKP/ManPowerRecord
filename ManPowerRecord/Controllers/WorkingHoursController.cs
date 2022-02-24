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
    public class WorkingHoursController : Controller
    {
        IWorkingHours WorkingHoursService;
        ICalculateWorkingHours CalculateOTService;

        public WorkingHoursController()
        {
            this.WorkingHoursService = new WorkingHoursService();
            this.CalculateOTService = new CalculateOvertimeService();
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
            for(int i = 0; i < whs.Count; i++)
            {
                whs[i] = CalculateOTService.CalculateOvertime(whs[i]);
            }
            return Json(whs);
        }

        [HttpPost]
        public JsonResult AddWorkingHours(string wh_string)
        {
            WorkingHoursModel wh = JsonConvert.DeserializeObject<WorkingHoursModel>(wh_string);
            var result = WorkingHoursService.AddWorkingHours(wh);
            return Json(result);
        }
    }
}
