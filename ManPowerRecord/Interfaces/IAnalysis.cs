using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Interfaces
{
    interface IAnalysis
    {
        List<TaskTotalHoursModel> GetTasksWorkingHours(string job_id);
        List<JobInvolveModel> GetPercentsInvolve(string job_id);
    }
}
