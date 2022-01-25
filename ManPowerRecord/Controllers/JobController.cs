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
    public class JobController : Controller
    {
        IJob JobService;

        public JobController()
        {
            this.JobService = new JobService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetJobs()
        {
            List<JobModel> jobs = JobService.GetAllJobs();
            return Json(jobs);
        }

        [HttpPost]
        public JsonResult AddJob(string job_string)
        {
            JobModel job = JsonConvert.DeserializeObject<JobModel>(job_string);
            var result = JobService.CreateJob(job);
            return Json(result);
        }
    }
}
