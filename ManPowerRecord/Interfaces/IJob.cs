using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Interfaces
{
    interface IJob
    {
        List<JobModel> GetAllJobs();
        string CreateJob(JobModel job);
        string UpdateJob(JobModel job);
    }
}
