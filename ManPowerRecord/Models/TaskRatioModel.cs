using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Models
{
    public class TaskRatioModel
    {
        string job_id { get; set; }
        string task { get; set; }
        double percentage { get; set; }
    }
}
