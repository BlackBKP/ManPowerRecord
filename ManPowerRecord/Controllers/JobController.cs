using ManPowerRecord.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<JobModel> GetJobs()
        {
            List<JobModel> jobs = new List<JobModel>();



            return jobs;
        }
    }
}
