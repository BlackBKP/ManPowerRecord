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
    public class AnalysisController : Controller
    {
        IAnalysis AnalysisService;

        public AnalysisController()
        {
            this.AnalysisService = new AnalysisService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetTasksRatio(string job_id)
        {
            List<TaskTotalHoursModel> tasks = AnalysisService.GetTasksWorkingHours(job_id);
            return Json(tasks);
        }

        [HttpGet]
        public JsonResult GetPercentInvolve(string job_id)
        {
            List<JobInvolveModel> invs = AnalysisService.GetPercentsInvolve(job_id);
            return Json(invs);
        }
    }
}
