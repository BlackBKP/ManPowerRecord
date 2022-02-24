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
    public class MonthlyWorkingHoursController : Controller
    {
        IWorkingHours WorkingHoursService;
        ICalculateWorkingHours CalculateOTService;
        static List<WorkingHoursModel> monthly = new List<WorkingHoursModel>();

        public MonthlyWorkingHoursController()
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
            DateTime date = DateTime.Today;
            int days = DateTime.DaysInMonth(date.Year, date.Month);
            List<WorkingHoursModel> whs = WorkingHoursService.GetWorkingHours(date.Year.ToString(), date.Month.ToString().PadLeft(2,'0'));
            for(int i = 0; i < whs.Count; i++)
            {
                whs[i] = CalculateOTService.CalculateOvertime(whs[i]);
            }
            List<WorkingHoursModel> total = new List<WorkingHoursModel>();
            for (int i = 1; i <= days; i++)
            {
                DateTime dd = new DateTime(date.Year, date.Month, i);
                WorkingHoursModel wh = new WorkingHoursModel()
                {
                    working_date = dd,
                    job_id = whs.Where(w => w.working_date == dd).Select(s => s.job_id).FirstOrDefault() ,
                    start_time = whs.Where(w => w.working_date == dd).Select(s => s.start_time).FirstOrDefault(),
                    stop_time = whs.Where(w => w.working_date == dd).Select(s => s.stop_time).FirstOrDefault(),
                    lunch = whs.Where(w => w.working_date == dd).Select(s => s.lunch).FirstOrDefault(),
                    dinner = whs.Where(w => w.working_date == dd).Select(s => s.dinner).FirstOrDefault(),
                    normal = whs.Where(w => w.working_date == dd).Select(s => s.normal).FirstOrDefault(),
                    ot1_5 = whs.Where(w => w.working_date == dd).Select(s => s.ot1_5).FirstOrDefault(),
                    ot3_0 = whs.Where(w => w.working_date == dd).Select(s => s.ot3_0).FirstOrDefault(),
                };
                total.Add(wh);
            }
            monthly = total;
            return Json(total);
        }

        [HttpGet]
        public JsonResult GetMonthlySummary()
        {
            List<string> job_ids = monthly.Select(s => s.job_id).Distinct().ToList();
            job_ids = job_ids.Where(w => w != null).ToList();
            List<JobWorkingHoursSummaryModel> jwhs = new List<JobWorkingHoursSummaryModel>();

            TimeSpan total_normal = new TimeSpan();
            TimeSpan total_ot1_5 = new TimeSpan();
            TimeSpan total_ot3_0 = new TimeSpan();
            for(int i = 0; i < job_ids.Count; i++)
            {
                TimeSpan nn = new TimeSpan();
                TimeSpan ot1_5 = new TimeSpan();
                TimeSpan ot3_0 = new TimeSpan();
                List<WorkingHoursModel> job = monthly.Where(w => w.job_id == job_ids[i]).ToList();
                for(int j = 0; j < job.Count; j++)
                {
                    nn += job[j].normal;
                    ot1_5 += job[j].ot1_5;
                    ot3_0 += job[j].ot3_0;
                }
                total_normal += nn;
                total_ot1_5 += ot1_5;
                total_ot3_0 += ot3_0;
                JobWorkingHoursSummaryModel jwh = new JobWorkingHoursSummaryModel()
                {
                    job_id = job_ids[i],
                    normal_hours = Convert.ToInt32(Math.Floor(nn.TotalHours)),
                    normal_min = Convert.ToInt32(nn.Minutes),
                    ot1_5_hours = Convert.ToInt32(Math.Floor(ot1_5.TotalHours)),
                    ot1_5_min = Convert.ToInt32(ot1_5.Minutes),
                    ot3_0_hours = Convert.ToInt32(Math.Floor(ot3_0.TotalHours)),
                    ot3_0_min = Convert.ToInt32(ot3_0.Minutes)
                };
                jwhs.Add(jwh);
            }
            jwhs.Add(new JobWorkingHoursSummaryModel()
            {
                job_id = "Total",
                normal_hours = Convert.ToInt32(total_normal.TotalHours),
                normal_min = Convert.ToInt32(total_normal.Minutes),
                ot1_5_hours = Convert.ToInt32(total_ot1_5.TotalHours),
                ot1_5_min = Convert.ToInt32(total_ot1_5.Minutes),
                ot3_0_hours = Convert.ToInt32(total_ot3_0.TotalHours),
                ot3_0_min = Convert.ToInt32(total_ot3_0.Minutes),
            });
            jwhs.Where(w => w.job_id != null).ToList();
            return Json(jwhs);
        }
    }
}
